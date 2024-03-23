using Microsoft.AspNetCore.Mvc;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.API.Endpoints;
public static class MusicasExtensions
{
    public static void AddEndpointsMusicas(this WebApplication app)
    {
        app.MapGet("/Musicas", ([FromServices] DAL<Musica> dal) =>
        {
            IEnumerable<Musica> listaDeMusicas = dal.Listar();

            if (listaDeMusicas is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(listaDeMusicas);
        });

        app.MapGet("Musicas/{nome}", ([FromServices] DAL<Musica> dal, string nome) =>
        {
            List<Musica> listaDemusica = [.. dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()))]!;

            if (listaDemusica.Count == 0)
            {
                return Results.NotFound();
            }

            return Results.Ok(listaDemusica);
        });

        app.MapPost("/Musicas", ([FromServices] DAL<Musica> dal, [FromBody] Musica musica) =>
        {
            dal.Adicionar(musica);
            return Results.Ok();
        });

        app.MapPut("/Musicas", ([FromServices] DAL<Musica> dal, [FromBody] Musica musica) =>
        {
            List<Musica> listaDeMusicas = [.. dal.RecuperarPor(a => a.Id == musica.Id)];

            if (listaDeMusicas.Count == 0)
            {
                return Results.NotFound();
            }

            Musica musicaAtualizar = listaDeMusicas[0];
            musicaAtualizar.Artista = musica.Artista;
            musicaAtualizar.AnoLancamento = musica.AnoLancamento;
            musicaAtualizar.Nome = musica.Nome;

            dal.Atualizar(musicaAtualizar);

            return Results.Ok(musicaAtualizar);
        });

        app.MapDelete("/Musicas/{id}", ([FromServices] DAL<Musica> dal, int id) =>
        {
            List<Musica> listaDeMusicas = [.. dal.RecuperarPor(a => a.Id == id)];

            if (listaDeMusicas.Count == 0)
            {
                return Results.NotFound();
            }

            Musica musica = listaDeMusicas[0];
            dal.Deletar(musica);

            return Results.NoContent();
        });

    }
}
