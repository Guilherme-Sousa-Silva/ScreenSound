namespace ScreenSound.API.Requests
{
    public record GeneroRequest(string Nome, string Descricao) { }
    public record GeneroRequestEdit(int Id, string Nome, string Descricao) { }
}
