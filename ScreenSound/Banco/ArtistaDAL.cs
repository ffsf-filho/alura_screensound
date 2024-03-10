using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;

namespace ScreenSound.Banco;

internal class ArtistaDAL
{
    public IEnumerable<Artista> Listar()
    {
        List<Artista> lista = new();

        try
        {
            string sql = "SELECT * FROM Artistas";

            using ScreenSoundContext context = new();
            lista = context.Artistas.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return lista;
    }

    //public void Adicionar(Artista artista)
    //{
    //    try
    //    {
    //        string sql = "INSERT INTO Artistas (Nome, Bio, FotoPerfil) VALUES (@nome, @bio, @perfilPadrao)";
            
    //        using SqlConnection connection = new ScreenSoundContext().ObterConexao();
    //        connection.Open();

    //        using SqlCommand command = new(sql, connection);
    //        command.Parameters.AddWithValue("@nome", artista.Nome);
    //        command.Parameters.AddWithValue("@bio", artista.Bio);
    //        command.Parameters.AddWithValue("@perfilPadrao", artista.FotoPerfil);

    //        int retorno = command.ExecuteNonQuery();
    //        Console.WriteLine($"Linhas afetadas: {retorno}");
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine(ex.Message);
    //    }
    //}

    //public void Atualizar(Artista artista)
    //{
    //    try
    //    {
    //        string sql = "UPDATE Artistas SET Nome = @nome, Bio = @bio, FotoPerfil = @perfilPadrao WHERE Id = @id";

    //        using SqlConnection connection = new ScreenSoundContext().ObterConexao();
    //        connection.Open();

    //        using SqlCommand command = new(sql, connection);
    //        command.Parameters.AddWithValue("@nome", artista.Nome);
    //        command.Parameters.AddWithValue("@bio", artista.Bio);
    //        command.Parameters.AddWithValue("@perfilPadrao", artista.FotoPerfil);
    //        command.Parameters.AddWithValue("@id", artista.Id);

    //        int retorno = command.ExecuteNonQuery();
    //        Console.WriteLine($"Linhas afetadas: {retorno}");
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine(ex.Message);
    //    }
    //}

    //public void Deletar(Artista artista)
    //{
    //    try
    //    {
    //        string sql = "DELETE FROM Artistas WHERE Id = @id";

    //        using SqlConnection connection = new ScreenSoundContext().ObterConexao();
    //        connection.Open();

    //        using SqlCommand command = new(sql, connection);
    //        command.Parameters.AddWithValue("@id", artista.Id);

    //        int retorno = command.ExecuteNonQuery();
    //        Console.WriteLine($"Linhas afetadas: {retorno}");
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine(ex.Message);
    //    }
    //}
}
