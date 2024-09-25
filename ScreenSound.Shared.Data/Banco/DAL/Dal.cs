using Microsoft.EntityFrameworkCore;

namespace ScreenSound.Banco.DAL
{
    public class Dal<T> where T : class
    {
        protected readonly ScreenSoundContext _context;

        public Dal(ScreenSoundContext context)
        {
            _context = context;
        }

        public IEnumerable<T> Listar()
        {
            return _context.Set<T>().ToList();
        }

        public T? GetBy(Func<T, bool> condicao)
        {
            return _context.Set<T>().FirstOrDefault(condicao);
        }
        public T? GetBy(int id)
        {
            return _context.Set<T>().FirstOrDefault(e => EF.Property<int>(e, "Id") == id);
        }

        public void Adicionar(T t)
        {
            _context.Set<T>().Add(t);
            _context.SaveChanges();
        }
        public void Editar(T t)
        {
            _context.Set<T>().Update(t);
            _context.SaveChanges();
        }
        public void Deletar(int id)
        {
            var remove = GetBy(id);
            _context.Set<T>().Remove(remove);
            _context.SaveChanges();
        }
    }
}
