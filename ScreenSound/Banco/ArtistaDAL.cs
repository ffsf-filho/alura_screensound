using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;

namespace ScreenSound.Banco;

internal class ArtistaDAL(ScreenSoundContext context)
{
    private readonly ScreenSoundContext _context = context;

    public IEnumerable<Artista> Listar()
    {
        List<Artista> lista = [];

        try
        {
            lista = [.. _context.Artistas];
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
