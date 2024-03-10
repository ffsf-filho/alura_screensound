using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;

namespace ScreenSound.Banco;

internal class Connection
{
    private readonly string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ScreenSound;Integrated Security=True;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
    //private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ScreenSound;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

    public SqlConnection ObterConexao()
    {
        return new SqlConnection(connectionString);
    }
}
