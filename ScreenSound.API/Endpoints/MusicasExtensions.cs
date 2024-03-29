﻿using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
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

        app.MapPost("/Musicas", ([FromServices] DAL<Musica> dal, [FromBody] MusicaRequest musicaRequest) =>
        {
            Musica musica = new(musicaRequest.nome)
            {
                ArtistaId = musicaRequest.ArtistaId,
                AnoLancamento = musicaRequest.anoLancamento,
            };
            dal.Adicionar(musica);
            return Results.Ok();
        });

        app.MapPut("/Musicas", ([FromServices] DAL<Musica> dal, [FromBody] Musica musicaRequestEdit) =>
        {
            List<Musica> listaDeMusicas = [.. dal.RecuperarPor(a => a.Id == musicaRequestEdit.Id)];

            if (listaDeMusicas.Count == 0)
            {
                return Results.NotFound();
            }

            Musica musicaAtualizar = listaDeMusicas[0];
            //musicaAtualizar.Artista = musicaRequestEdit.Artista;
            musicaAtualizar.AnoLancamento = musicaRequestEdit.AnoLancamento;
            musicaAtualizar.Nome = musicaRequestEdit.Nome;

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
