using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;

namespace ScreenSound.Banco.DAL
{
    public class ArtistaDal : Dal<Artista>
    {
        public ArtistaDal(ScreenSoundContext context): base(context) { }

        public Artista? GetByName(string name)
        {
            return _context.Artistas.FirstOrDefault(artista => artista.Nome.Equals(name));
        }
    }
}
