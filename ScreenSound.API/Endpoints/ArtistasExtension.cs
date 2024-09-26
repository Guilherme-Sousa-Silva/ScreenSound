using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.Banco.DAL;
using ScreenSound.Modelos;

namespace ScreenSound.API.Endpoints
{
    public static class ArtistasExtension
    {
        public static void AddEndpointsArtistas(this WebApplication app)
        {

            #region Endpoint de Artista
            app.MapGet("/Artistas", ([FromServices] Dal<Artista> dal) =>
            {
                var artistas = dal.Listar();
                return Results.Ok(artistas);
            });

            app.MapGet("/Artistas/{nome}", ([FromServices] Dal<Artista> dal, [FromRoute] string nome) =>
            {
                var artista = dal.GetBy(x => x.Nome.ToUpper().Equals(nome.ToUpper()));

                if (artista is null)
                {
                    return Results.NotFound($"Artista não encontrado pelo nome {nome}");
                }

                return Results.Ok(artista);
            });

            app.MapPost("/Artistas/", ([FromServices] Dal<Artista> dal, [FromBody] ArtistaRequest artista) =>
            {
                if (artista is null)
                {
                    return Results.BadRequest("Artista não pode ser nulo");
                }
                if (string.IsNullOrEmpty(artista.Nome) || string.IsNullOrEmpty(artista.Bio))
                {
                    return Results.BadRequest("Requisição inválida. Por favor, revise os dados enviados!");
                }

                Artista artistaCriar = new Artista(artista.Nome, artista.Bio);

                dal.Adicionar(artistaCriar);
                return Results.Ok("Artista criado com sucesso!");
            });

            app.MapPut("/Artistas/", ([FromServices] Dal<Artista> dal, [FromBody] ArtistaEditRequest artista) =>
            {
                if (artista is null)
                {
                    return Results.BadRequest("Artista não pode ser nulo");
                }
                if (string.IsNullOrEmpty(artista.Nome) || string.IsNullOrEmpty(artista.Bio) || string.IsNullOrEmpty(artista.FotoPerfil))
                {
                    return Results.BadRequest("Requisição inválida. Por favor, revise os dados enviados!");
                }

                Artista artistaEditar = new Artista(artista.Nome, artista.Bio, artista.FotoPerfil);
                dal.Editar(artistaEditar);
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
        }
    }
}
