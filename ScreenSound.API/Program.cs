using Microsoft.AspNetCore.Mvc;
using ScreenSound.Banco;
using ScreenSound.Modelos;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(
    options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

app.MapGet("/Artistas", () =>
{
    DAL<Artista> dal = new(new ScreenSoundContext());
    IEnumerable<Artista> lista =  dal.Listar();

    if (lista is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(lista);
});

app.MapGet("/Artistas/{nome}", (string nome) =>
{
    DAL<Artista> dal = new(new ScreenSoundContext());
    List<Artista> artista =  [.. dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()))]!;

    if(artista is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(artista);
});

app.MapPost("Artistas", ([FromBody] Artista artista) =>
{
    DAL<Artista> dal = new(new ScreenSoundContext());
    dal.Adicionar(artista);
    return Results.Ok();
});

app.Run();
