using ScreenSound.Banco.DAL;
using ScreenSound.Modelos;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => 
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
var app = builder.Build();

app.MapGet("/", () =>
{
    var artistaDal = new Dal<Artista>(new ScreenSound.Banco.ScreenSoundContext());
    return artistaDal.Listar();
});

app.Run();
