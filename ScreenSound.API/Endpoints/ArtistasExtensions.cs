using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.Banco;
using ScreenSound.Shared.Modelos.Modelos;

namespace ScreenSound.API.Endpoints;

public static class ArtistasExtensions
{
    public static void AddEndpointsArtistas(this WebApplication app)
    {
        app.MapGet("/Artistas", ([FromServices] DAL<Artista> dal) =>
        {
            IEnumerable<Artista> listaDeArtistas = dal.Listar();

            if (listaDeArtistas is null)
            {
                return Results.NotFound();
            }
            List<ArtistaResponse> listaDeArtistaResponse = [.. EntityListToResponseList(listaDeArtistas)];

            return Results.Ok(listaDeArtistaResponse);
        });

        app.MapGet("/Artistas/{nome}", ([FromServices] DAL<Artista> dal, string nome) =>
        {
            Artista artista = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()))!;

            if (artista is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(EntityToResponse(artista));
        });

        app.MapPost("/Artistas", async ([FromServices] IHostEnvironment env, [FromServices] DAL<Artista> dal, [FromBody] ArtistaRequest artistaRequest) =>
        {
            string nome = artistaRequest.Nome.Trim();
            string imagemArtista = $"{DateTime.Now.ToString("ddMMyyyyhhss")}.{nome}.jpeg";
            string path = Path.Combine(env.ContentRootPath, "wwwroot", "FotosPerfil", imagemArtista);

            using MemoryStream ms = new MemoryStream(Convert.FromBase64String(artistaRequest.FotoPerfil!));
            using FileStream fs = new(path, FileMode.Create);
            await ms.CopyToAsync(fs);

            Artista artista = new(artistaRequest.Nome, artistaRequest.Bio, $"FotosPerfil/{imagemArtista}");
            dal.Adicionar(artista);
            return Results.Ok();
        });

        app.MapDelete("/Artistas/{id}", ([FromServices] DAL<Artista> dal, int id) =>
        {
            Artista artista = dal.RecuperarPor(a => a.Id == id)!;

            if (artista is null)
            {
                return Results.NotFound();
            }

            dal.Deletar(artista);

            return Results.NoContent();
        });

        app.MapPut("/Artistas", ([FromServices] DAL<Artista> dal, [FromBody] Artista artistaRequestEdit) =>
        {
            Artista artista = dal.RecuperarPor(a => a.Id == artistaRequestEdit.Id)!;

            if (artista is null)
            {
                return Results.NotFound();
            }

            artista.Nome = artistaRequestEdit.Nome;
            artista.Bio = artistaRequestEdit.Bio;
            artista.FotoPerfil = artistaRequestEdit.FotoPerfil;

            dal.Atualizar(artista);

            return Results.Ok(artista);
        });
    }

    private static ICollection<ArtistaResponse> EntityListToResponseList(IEnumerable<Artista> listaDeArtistas)
    {
        return listaDeArtistas.Select(a => EntityToResponse(a)).ToList();
    }

    private static ArtistaResponse EntityToResponse(Artista artista)
    {
        return new ArtistaResponse(artista.Id, artista.Nome, artista.Bio, artista.FotoPerfil);
    }
}
