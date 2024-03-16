using ScreenSound.Banco;
using ScreenSound.Modelos;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(
    options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

app.MapGet("/", () =>
{
    DAL<Artista> dal = new(new ScreenSoundContext());
    IEnumerable<Artista> lista =  dal.Listar();

    return lista;
});

app.Run();
