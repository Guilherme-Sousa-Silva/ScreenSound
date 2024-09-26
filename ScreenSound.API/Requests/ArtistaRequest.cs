namespace ScreenSound.API.Requests
{
    public record ArtistaRequest(string Nome, string Bio, string FotoPerfil);
    public record ArtistaEditRequest(int Id, string Nome, string Bio, string FotoPerfil);
}
