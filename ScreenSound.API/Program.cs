using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ScreenSound.API.Endpoints;
using ScreenSound.Banco;
using ScreenSound.Shared.Modelos.Modelos;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration(config =>
{
    var settings = config.Build();
    config.AddAzureAppConfiguration("Endpoint=https://screensoundfco-configuration.azconfig.io;Id=r5mV;Secret=1glVJfWcm9kqBDet8ev6CkuI1l4fqUeM9/EXO1DKbvM=");
});

builder.Services.AddDbContext<ScreenSoundContext>((options) =>
{
    options
    .UseSqlServer(builder.Configuration["ConnectionString:ScrrenSoundDB"])
    .UseLazyLoadingProxies();
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
    });
});

builder.Services.AddTransient<DAL<Artista>>();
builder.Services.AddTransient<DAL<Musica>>();
builder.Services.AddTransient<DAL<Genero>>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(
    options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();
app.UseCors();
app.UseStaticFiles();

app.AddEndpointsArtistas();
app.AddEndpointsMusicas();
app.AddEndPointGeneros();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
