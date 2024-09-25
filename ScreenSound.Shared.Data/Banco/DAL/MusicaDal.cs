using ScreenSound.Modelos;

namespace ScreenSound.Banco.DAL
{
    public class MusicaDal: Dal<Musica>
    {
        public MusicaDal(ScreenSoundContext context) :base(context) { }
    }
}
