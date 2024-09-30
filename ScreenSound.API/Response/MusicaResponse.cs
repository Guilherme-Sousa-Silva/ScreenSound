using ScreenSound.Shared.Modelos.Modelos;

namespace ScreenSound.API.Response
{
    public record MusicaResponse(int Id, string Nome, int? AnoLancamento, string NomeArtista) { }
}
