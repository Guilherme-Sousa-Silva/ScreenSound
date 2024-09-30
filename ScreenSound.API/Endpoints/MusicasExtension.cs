using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.Banco.DAL;
using ScreenSound.Modelos;
using ScreenSound.Shared.Modelos.Modelos;

namespace ScreenSound.API.Endpoints
{
    public static class MusicasExtension
    {
        public static void AddEndpointsMusicas(this WebApplication app)
        {

            #region Endpoint de Musicas
            app.MapGet("/Musicas", ([FromServices] Dal<Musica> dal) =>
            {
                var musicas =  dal.Listar();
                return Results.Ok(EntityListToResponseList(musicas.ToList()));
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

            app.MapPost("/Musicas", ([FromServices] Dal<Musica> dal, [FromServices] Dal<Genero> generoDal, [FromBody] MusicaRequest musica) =>
            {
                if (musica is null)
                {
                    return Results.BadRequest("Musica não pode ser nulo!");
                }

                if (string.IsNullOrEmpty(musica.Nome) || string.IsNullOrEmpty(musica.AnoLancamento.ToString()) || string.IsNullOrEmpty(musica.ArtistaId.ToString()))
                {
                    return Results.BadRequest("Requisição inválida. Por favor, revise os dados enviados!");
                }

                Musica musicaCriar = new Musica(
                    musica.Nome, 
                    musica.AnoLancamento, 
                    musica.ArtistaId
                    )
                {
                    Generos = musica.Generos is not null ? GeneroRequestConverter(musica.Generos, generoDal) : new List<Genero>()
                };

                dal.Adicionar(musicaCriar);
                return Results.Ok("Musica adicionada com sucesso!");
            });

            app.MapPut("/Musicas", ([FromServices] Dal<Musica> dal, [FromBody] MusicaEditRequest musica) =>
            {
                if (musica is null)
                {
                    return Results.BadRequest("Musica não pode ser nulo!");
                }

                if (string.IsNullOrEmpty(musica.Nome) || string.IsNullOrEmpty(musica.AnoLancamento.ToString()) || string.IsNullOrEmpty(musica.ArtistaId.ToString()))
                {
                    return Results.BadRequest("Requisição inválida. Por favor, revise os dados enviados!");
                }

                Musica musicaEditar = new Musica(musica.Nome, musica.AnoLancamento, musica.ArtistaId);
                dal.Editar(musicaEditar);
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
        }

        private static List<Genero> GeneroRequestConverter(ICollection<GeneroRequest> generos, Dal<Genero> dalGenero)
        {
            var listaDeGeneros = new List<Genero>();
            foreach (var item in generos)
            {
                var entity = RequestToEntity(item);
                var genero = dalGenero.GetBy(g => g.Nome.ToUpper().Equals(item.Nome.ToUpper()));
                if (genero is not null)
                {
                    listaDeGeneros.Add(genero);
                }
                else
                {
                    listaDeGeneros.Add(entity);
                }
            }

            return listaDeGeneros;
        }

        private static Genero RequestToEntity(GeneroRequest genero)
        {
            return new Genero() { Nome = genero.Nome, Descricao = genero.Descricao };
        }

        private static ICollection<MusicaResponse> EntityListToResponseList(List<Musica> musicaList)
        {
            return musicaList.Select(a => EntityToResponse(a)).ToList();
        }

        private static MusicaResponse EntityToResponse(Musica musica)
        {
            return new MusicaResponse(musica.Id, musica.Nome!, musica.Artista!.Id, musica.Artista.Nome);
        }
    }
}
