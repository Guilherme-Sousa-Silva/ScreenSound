namespace ScreenSound.API.Requests
{
    public record MusicaRequest(string Nome, int AnoLancamento, int ArtistaId);
    public record MusicaEditRequest(int Id, string Nome, int AnoLancamento, int ArtistaId);
}
