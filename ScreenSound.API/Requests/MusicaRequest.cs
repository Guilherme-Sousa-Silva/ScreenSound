using ScreenSound.Shared.Modelos.Modelos;

namespace ScreenSound.API.Requests
{
    public record MusicaRequest(string Nome, int AnoLancamento, int ArtistaId, List<GeneroRequest> Generos = null);
    public record MusicaEditRequest(int Id, string Nome, int AnoLancamento, int ArtistaId, List<GeneroRequest> Generos = null);
}
