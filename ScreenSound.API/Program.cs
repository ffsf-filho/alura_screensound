using Microsoft.AspNetCore.Mvc;
using ScreenSound.Banco;
using ScreenSound.Modelos;
using System.Data.SqlTypes;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ScreenSoundContext>();
builder.Services.AddTransient<DAL<Artista>>();
builder.Services.AddTransient<DAL<Musica>>();
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(
    options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

app.MapGet("/Artistas", ([FromServices] DAL<Artista> dal) =>
{
    IEnumerable<Artista> lista =  dal.Listar();

    if (lista is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(lista);
});

app.MapGet("/Artistas/{nome}", ([FromServices] DAL < Artista > dal, string nome) =>
{
    List<Artista> artista =  [.. dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()))]!;

    if(artista.Count == 0)
    {
        return Results.NotFound();
    }

    return Results.Ok(artista);
});

app.MapPost("/Artistas", ([FromServices] DAL < Artista > dal, [FromBody] Artista artista) =>
{
    dal.Adicionar(artista);
    return Results.Ok();
});

app.MapDelete("/Artistas/{id}", ([FromServices] DAL<Artista> dal, int id) =>
{
    List<Artista> listaArtista = [..dal.RecuperarPor(a => a.Id == id)];

    if(listaArtista.Count == 0)
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

app.MapGet("/Musica", ([FromServices] DAL<Musica> dal) =>
{
    IEnumerable<Musica> listaDeMusicas = dal.Listar();

    if (listaDeMusicas is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(listaDeMusicas);
});

app.MapGet("Musica/{nome}", ([FromServices] DAL<Musica> dal, string nome) =>
{
    List<Musica> listaDemusica = [.. dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()))]!;

    if (listaDemusica.Count == 0)
    {
        return Results.NotFound();
    }

    return Results.Ok(listaDemusica);
});

app.MapPost("/Musica", ([FromServices] DAL<Musica> dal, [FromBody] Musica musica) => 
{
    dal.Adicionar(musica);
    return Results.Ok();
});

app.MapPut("/Musica", ([FromServices] DAL < Musica > dal, [FromBody] Musica musica) =>
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

app.MapDelete("/Musica/{id}", ([FromServices] DAL<Musica> dal, int id) =>
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

app.Run();
