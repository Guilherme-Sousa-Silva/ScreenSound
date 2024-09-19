using ScreenSound.Banco.DAL;
using ScreenSound.Modelos;

namespace ScreenSound.Menus;

internal class MenuMostrarArtistas : Menu
{
    public override void Executar(Dal<Artista> ArtistaDal)
    {
        base.Executar(ArtistaDal);
        ExibirTituloDaOpcao("Exibindo todos os artistas registradas na nossa aplicação");

        foreach (Artista artista in ArtistaDal.Listar())
        {
            Console.WriteLine($"Artista: {artista.Nome}");
        }

        Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
        Console.ReadKey();
        Console.Clear();
    }
}
