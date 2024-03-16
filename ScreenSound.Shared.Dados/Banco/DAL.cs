namespace ScreenSound.Banco
{
    public class DAL<T>(ScreenSoundContext context) where T : class
    {
        protected readonly ScreenSoundContext _context = context;

        public IEnumerable<T> Listar()
        {
            List<T> lista = [];

            try
            {
                lista = [.. _context.Set<T>()];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }

            return lista;
        }

        public void Adicionar(T objeto)
        {
            try
            {
                _context.Set<T>().Add(objeto);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }

        public void Atualizar(T objeto)
        {
            try
            {
                _context.Set<T>().Update(objeto);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }

        public void Deletar(T objeto)
        {
            try
            {
                _context.Set<T>().Remove(objeto);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }

        public T? RecuperarPor(Func<T, bool> condicao)
        {
            T? artista = null;

            try
            {
                artista = _context.Set<T>().FirstOrDefault(condicao);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return artista;
        }

        public IEnumerable<T>? ListarPor(Func<T, bool> condicao)
        {
            IEnumerable<T>? lista = null;

            try
            {
                lista = [.. _context.Set<T>().Where( condicao)];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return lista;
        }
    }
}
