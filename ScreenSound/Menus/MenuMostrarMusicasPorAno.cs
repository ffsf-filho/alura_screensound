using Microsoft.EntityFrameworkCore;
using ScreenSound.Banco;
using ScreenSound.Shared.Modelos.Modelos;

namespace ScreenSound.Menus;

internal class MenuMostrarMusicasPorAno : Menu
{
    public override void Executar(DAL<Artista> artistaDAL)
    {
        base.Executar(artistaDAL);

        ExibirTituloDaOpcao("Mostrar musicas por ano de lançamento");
        Console.Write("Digite o ano para consultar músicas:");
        string anoLancamento = Console.ReadLine()!;

		DbContextOptions options = new DbContextOptionsBuilder<ScreenSoundContext>()
	                    .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ScreenSoundV0;Integrated Security=True;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False")
	                    .UseLazyLoadingProxies()
	                    .Options;

		var musicaDal = new DAL<Musica>(new ScreenSoundContext(options));
        var listaAnoLancamento = musicaDal.ListarPor(a => a.AnoLancamento == Convert.ToInt32(anoLancamento))!;

        if (listaAnoLancamento.Any())
        {
            Console.WriteLine($"\nMusicas do Ano {anoLancamento}:");

            foreach (var musica in listaAnoLancamento)
            {
                musica.ExibirFichaTecnica();
            }

            Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
        }
        else
        {
            Console.WriteLine($"\nO ano {anoLancamento} não foi encontrada!");
            Console.WriteLine("Digite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
