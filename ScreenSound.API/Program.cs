using Microsoft.AspNetCore.Mvc;
using ScreenSound.Banco;
using ScreenSound.Banco.DAL;
using ScreenSound.Modelos;
using System.Data.SqlTypes;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ScreenSoundContext>();
builder.Services.AddTransient<Dal<Artista>>();
builder.Services.AddTransient<Dal<Musica>>();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => 
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
var app = builder.Build();

#region Endpoint de Artista
app.MapGet("/Artistas", ([FromServices] Dal<Artista> dal) =>
{
    var artistas = dal.Listar();
    return Results.Ok(artistas);
});

app.MapGet("/Artistas/{nome}", ([FromServices] Dal < Artista > dal, [FromRoute]string nome) =>
{
    var artista = dal.GetBy(x => x.Nome.ToUpper().Equals(nome.ToUpper()));

    if(artista is null)
    {
        return Results.NotFound($"Artista não encontrado pelo nome {nome}");
    }

    return Results.Ok(artista);
});

app.MapPost("/Artistas/", ([FromServices] Dal < Artista > dal, [FromBody] Artista artista) =>
{
    if (artista is null)
    {
        return Results.BadRequest("Artista não pode ser nulo");
    }
    if (string.IsNullOrEmpty(artista.Nome) || string.IsNullOrEmpty(artista.Bio) || string.IsNullOrEmpty(artista.FotoPerfil))
    {
        return Results.BadRequest("Requisição inválida. Por favor, revise os dados enviados!");
    }

    dal.Adicionar(artista);
    return Results.Ok("Artista criado com sucesso!");
});

app.MapPut("/Artistas/", ([FromServices] Dal<Artista> dal, [FromBody] Artista artista) =>
{
    if (artista is null)
    {
        return Results.BadRequest("Artista não pode ser nulo");
    }
    if (string.IsNullOrEmpty(artista.Nome) || string.IsNullOrEmpty(artista.Bio) || string.IsNullOrEmpty(artista.FotoPerfil))
    {
        return Results.BadRequest("Requisição inválida. Por favor, revise os dados enviados!");
    }

    dal.Editar(artista);
    return Results.Ok("Artista editado com sucesso!");
});

app.MapDelete("/Artistas/{id}", ([FromServices] Dal<Artista> dal, [FromRoute] int id) =>
{
    if (id <= 0)
    {
        return Results.BadRequest("Id do artista não pode ser nulo!");
    }

    dal.Deletar(id);
    return Results.Ok("Artista deletado com sucesso!");
});
#endregion

#region Endpoint de Musicas
app.MapGet("/Musicas", ([FromServices] Dal<Musica> dal) =>
{
    return Results.Ok(dal.Listar());
});

app.MapGet("Musicas/{musica}", ([FromServices] Dal<Musica> dal, [FromRoute] string musica) =>
{
    if (string.IsNullOrEmpty(musica))
    {
        return Results.BadRequest("Por favor, informe o nome da musica!");
    }

    var musicaEncontrada = dal.GetBy(a => a.Nome.ToUpper().Equals(musica.ToUpper()));

    if (musicaEncontrada is null)
    {
        return Results.NotFound($"Nenhuma musica encontrada pelo nome {musica}");
    }

    return Results.Ok(musicaEncontrada);
});

app.MapPost("/Musicas", ([FromServices] Dal<Musica> dal, [FromBody] Musica musica) =>
{
    if (musica is null)
    {
        return Results.BadRequest("Musica não pode ser nulo!");
    }

    if (string.IsNullOrEmpty(musica.Nome) || string.IsNullOrEmpty(musica.AnoLancamento.ToString()) || string.IsNullOrEmpty(musica.ArtistaId.ToString()))
    {
        return Results.BadRequest("Requisição inválida. Por favor, revise os dados enviados!");
    }

    dal.Adicionar(musica);
    return Results.Ok("Musica adicionada com sucesso!");
});

app.MapPut("/Musicas", ([FromServices] Dal<Musica> dal, [FromBody] Musica musica) =>
{
    if (musica is null)
    {
        return Results.BadRequest("Musica não pode ser nulo!");
    }

    if (string.IsNullOrEmpty(musica.Nome) || string.IsNullOrEmpty(musica.AnoLancamento.ToString()) || string.IsNullOrEmpty(musica.ArtistaId.ToString()))
    {
        return Results.BadRequest("Requisição inválida. Por favor, revise os dados enviados!");
    }

    dal.Editar(musica);
    return Results.Ok("Musica editada com sucesso!");
});

app.MapDelete("/Musicas/{id}", ([FromServices] Dal<Musica> dal, [FromRoute] int id) =>
{
    if (id <= 0)
    {
        return Results.BadRequest("Por favor, informe o id da musica");
    }

    dal.Deletar(id);
    return Results.Ok("Musica deletada com sucesso!");
});
#endregion

app.Run();
