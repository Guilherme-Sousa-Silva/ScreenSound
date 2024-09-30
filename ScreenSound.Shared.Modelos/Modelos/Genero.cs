using ScreenSound.Modelos;

namespace ScreenSound.Shared.Modelos.Modelos
{
    public class Genero
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public virtual List<Musica> Musicas { get; set; }

        public override string ToString()
        {
            return $"Nome: {Nome} - Descrição: {Descricao}";
        }
    }
}
