using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.Banco;
using ScreenSound.Shared.Modelos.Modelos;

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
            List<MusicaResponse> musicaListResponse = [.. EntityListToResponseList(listaDeMusicas)];
            return Results.Ok(musicaListResponse);
        });

        app.MapGet("Musicas/{nome}", ([FromServices] DAL<Musica> dal, string nome) =>
        {
            Musica musica = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()))!;

            if (musica is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(EntityToResponse(musica));
        });

        app.MapPost("/Musicas", ([FromServices] DAL<Musica> dalMusica, [FromServices] DAL<Genero> dalGenero, [FromBody] MusicaRequest musicaRequest) =>
        {
            Musica musica = new(musicaRequest.nome)
            {
                ArtistaId = musicaRequest.ArtistaId,
                AnoLancamento = musicaRequest.anoLancamento,
                Generos = musicaRequest.Generos is not null? GeneroRequestConverter(musicaRequest.Generos!, dalGenero) : [],
            };
            dalMusica.Adicionar(musica);
            return Results.Ok();
        });

        app.MapPut("/Musicas", ([FromServices] DAL<Musica> dal, [FromBody] Musica musicaRequestEdit) =>
        {
            Musica musica = dal.RecuperarPor(a => a.Id == musicaRequestEdit.Id)!;

            if (musica is null)
            {
                return Results.NotFound();
            }

            //musica.Artista = musicaRequestEdit.Artista;
            musica.AnoLancamento = musicaRequestEdit.AnoLancamento;
            musica.Nome = musicaRequestEdit.Nome;

            dal.Atualizar(musica);

            return Results.Ok(musica);
        });

        app.MapDelete("/Musicas/{id}", ([FromServices] DAL<Musica> dal, int id) =>
        {
            Musica musica = dal.RecuperarPor(a => a.Id == id)!;

            if (musica is null)
            {
                return Results.NotFound();
            }

            dal.Deletar(musica);

            return Results.NoContent();
        });

    }
    private static ICollection<Genero> GeneroRequestConverter(ICollection<GeneroRequest> generos, DAL<Genero> dalGenero)
    {
        List<Genero> listaDeGeneros = [];

        foreach (GeneroRequest generoRequest in generos)
        {
            Genero entityGenero = RequestToEntity(generoRequest);
            Genero genero = dalGenero.RecuperarPor(g => g.Nome!.ToUpper().Equals(generoRequest.Nome.ToUpper()))!;

            if(genero is not null)
            {
                listaDeGeneros.Add(genero);
            }
            else
            {
                listaDeGeneros.Add(entityGenero);
            }
        }
        
        return listaDeGeneros;
    }

    private static Genero RequestToEntity(GeneroRequest genero)
    {
        return new Genero() { Nome = genero.Nome, Descricao = genero.Descricao };
    }

    private static ICollection<MusicaResponse> EntityListToResponseList(IEnumerable<Musica> musicaList)
    {
        return musicaList.Select(a => EntityToResponse(a)).ToList();
    }

    private static MusicaResponse EntityToResponse(Musica musica)
    {
        return new MusicaResponse(musica.Id, musica.Nome!, musica.Artista!.Id, musica.Artista.Nome, musica!.AnoLancamento);
    }
}
