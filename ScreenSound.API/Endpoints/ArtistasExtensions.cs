using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.API.Endpoints;

public static class ArtistasExtensions
{
    public static void AddEndpointsArtistas(this WebApplication app)
    {
        app.MapGet("/Artistas", ([FromServices] DAL<Artista> dal) =>
        {
            IEnumerable<Artista> lista = dal.Listar();

            if (lista is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(lista);
        });

        app.MapGet("/Artistas/{nome}", ([FromServices] DAL<Artista> dal, string nome) =>
        {
            List<Artista> artista = [.. dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()))]!;

            if (artista.Count == 0)
            {
                return Results.NotFound();
            }

            return Results.Ok(artista);
        });

        app.MapPost("/Artistas", ([FromServices] DAL<Artista> dal, [FromBody] ArtistaRequest artistaRequest) =>
        {
            Artista artista = new(artistaRequest.nome, artistaRequest.bio);
            dal.Adicionar(artista);
            return Results.Ok();
        });

        app.MapDelete("/Artistas/{id}", ([FromServices] DAL<Artista> dal, int id) =>
        {
            List<Artista> listaArtista = [.. dal.RecuperarPor(a => a.Id == id)];

            if (listaArtista.Count == 0)
            {
                return Results.NotFound();
            }

            Artista artista = listaArtista[0];
            dal.Deletar(artista);

            return Results.NoContent();
        });

        app.MapPut("/Artistas", ([FromServices] DAL<Artista> dal, [FromBody] Artista artista) =>
        {
            List<Artista> listaArtista = [.. dal.RecuperarPor(a => a.Id == artista.Id)];

            if (listaArtista.Count == 0)
            {
                return Results.NotFound();
            }

            Artista artistaAtualizar = listaArtista[0];
            artistaAtualizar.Nome = artista.Nome;
            artistaAtualizar.Bio = artista.Bio;
            artistaAtualizar.FotoPerfil = artista.FotoPerfil;

            dal.Atualizar(artistaAtualizar);

            return Results.Ok(artistaAtualizar);
        });

    }
}
