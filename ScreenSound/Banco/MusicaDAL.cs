using ScreenSound.Modelos;

namespace ScreenSound.Banco;

internal class MusicaDAL(ScreenSoundContext context)
{
    private readonly ScreenSoundContext _context = context;

    public IEnumerable<Musica> Listar()
    {
        List<Musica> lista = [];

		try
		{
			lista = [.. _context.Musicas];
		}
		catch (Exception ex)
		{
            Console.WriteLine($"{ex.Message}");
        }

		return lista;
    }

	public void Adicionar(Musica musica)
	{
		try
		{
			_context.Musicas.Add(musica);
			_context.SaveChanges();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"{ex.Message}");
		}
	}

    public void Atualizar(Musica musica)
    {
        try
        {
            _context.Musicas.Update(musica);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }

    public void Deletar(Musica musica)
    {
        try
        {
            _context.Musicas.Remove(musica);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
    }

    public Musica? RecuperarPeloNome(string nomeDaMusica)
    {
        Musica musica = null;

        try
        {
            musica = _context.Musicas.FirstOrDefault(m => m.Nome.Equals(nomeDaMusica))!;
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
        }

        return musica;
    }
}
