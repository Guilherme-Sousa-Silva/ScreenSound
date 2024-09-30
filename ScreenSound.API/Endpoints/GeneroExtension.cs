using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.Banco.DAL;
using ScreenSound.Modelos;
using ScreenSound.Shared.Modelos.Modelos;

namespace ScreenSound.API.Endpoints
{
    public static class GeneroExtension
    {
        public static void AddEdnpointsGenero(this WebApplication app)
        {
            app.MapGet("/Generos", ([FromServices] Dal<Genero> dal) =>
            {
                var musicas = dal.Listar();
                return Results.Ok(EntityListToResponseList(musicas.ToList()));
            });

            app.MapPost("Generos", ([FromServices] Dal<Genero> dal, [FromBody] GeneroRequest generoRequest) =>
            {
                if (generoRequest is null)
                {
                    return Results.BadRequest("Musica não pode ser nulo!");
                }

                if (string.IsNullOrEmpty(generoRequest.Nome) || string.IsNullOrEmpty(generoRequest.Descricao.ToString()))
                {
                    return Results.BadRequest("Requisição inválida. Por favor, revise os dados enviados!");
                }

                Genero generoCriar = new Genero()
                {
                    Nome = generoRequest.Nome,
                    Descricao = generoRequest.Descricao,
                };

                dal.Adicionar(generoCriar);
                return Results.Ok("Genero criado com sucesso");
            });

            app.MapPut("Generos", ([FromServices] Dal<Genero> dal, [FromBody] GeneroRequestEdit generoRequest) =>
            {
                if (generoRequest is null)
                {
                    return Results.BadRequest("Musica não pode ser nulo!");
                }

                if (generoRequest.Id <= 0 || string.IsNullOrEmpty(generoRequest.Nome) || string.IsNullOrEmpty(generoRequest.Descricao.ToString()))
                {
                    return Results.BadRequest("Requisição inválida. Por favor, revise os dados enviados!");
                }

                Genero generoEditar = new Genero()
                {
                    Nome = generoRequest.Nome,
                    Descricao = generoRequest.Descricao,
                };

                dal.Editar(generoEditar);
                return Results.Ok("Genero editado com sucesso");
            });

            app.MapDelete("Generos/{id:int}", ([FromServices] Dal<Genero> dal, [FromRoute] int id) =>
            {
                if (id <= 0)
                {
                    return Results.BadRequest("Por favor, informe um id válido!");
                }

                dal.Deletar(id);
                return Results.Ok("Genero deletado com sucesso!");
            });
        }

        private static ICollection<GeneroResponse> EntityListToResponseList(List<Genero> generos)
        {
            return generos.Select(a => EntityToResponse(a)).ToList();
        }

        private static GeneroResponse EntityToResponse(Genero genero)
        {
            return new GeneroResponse(genero.Id, genero.Nome, genero.Descricao);
        }
    }
}
