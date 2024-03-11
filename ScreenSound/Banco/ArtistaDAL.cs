using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;

namespace ScreenSound.Banco;

internal class ArtistaDAL
{
    private readonly ScreenSoundContext _context;

    public ArtistaDAL(ScreenSoundContext context)
    {
        this._context = context;
    }

    public IEnumerable<Artista> Listar()
    {
        List<Artista> lista = new();

        try
        {
            lista = _context.Artistas.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return lista;
    }

    public void Adicionar(Artista artista)
    {
        try
        {
            _context.Artistas.Add(artista);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void Atualizar(Artista artista)
    {
        try
        {
            _context.Artistas.Update(artista);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void Deletar(Artista artista)
    {
        try
        {
            _context.Artistas.Remove(artista);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public Artista? RecuperarPeloNome(string nomeDoArtista)
    {
        Artista? artista = null;

        try
        {
            artista = _context.Artistas.FirstOrDefault(a => a.Nome.Equals(nomeDoArtista));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return artista;
    } 
}
