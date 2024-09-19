using ScreenSound.Banco.DAL;
using ScreenSound.Modelos;

namespace ScreenSound.Menus;

internal class MenuSair : Menu
{
    public override void Executar(Dal<Artista> ArtistaDal)
    {
        Console.WriteLine("Tchau tchau :)");
    }
}
