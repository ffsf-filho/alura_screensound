using Microsoft.AspNetCore.Mvc;
using ScreenSound.Banco;
using ScreenSound.Modelos;
using System.Data.SqlTypes;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ScreenSoundContext>();
builder.Services.AddTransient<DAL<Artista>>();
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

app.Run();
