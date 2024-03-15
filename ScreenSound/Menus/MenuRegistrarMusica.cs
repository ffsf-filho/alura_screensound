using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.Menus;

internal class MenuRegistrarMusica : Menu
{
    public override void Executar(DAL<Artista> artistaDAL)
    {
        base.Executar(artistaDAL);
        ExibirTituloDaOpcao("Registro de músicas");
        Console.Write("Digite o artista cuja música deseja registrar: ");
        
        string nomeDoArtista = Console.ReadLine()!;
        Artista artistaRecuperado = artistaDAL.RecuperarPor(a => a.Nome.Equals(nomeDoArtista))!;

        if (artistaRecuperado is not null)
        {
            Console.Write("Agora digite o título da música: ");
            string tituloDaMusica = Console.ReadLine()!;

            Console.Write("Agora digite o ano de laçamento da música: ");
            int anoLacamentoDaMusica = Convert.ToInt32(Console.ReadLine())!;

            artistaRecuperado.AdicionarMusica(new Musica(tituloDaMusica) { AnoLancamento = anoLacamentoDaMusica});

            Console.WriteLine($"A música {tituloDaMusica} de {nomeDoArtista} foi registrada com sucesso!");

            artistaDAL.Atualizar(artistaRecuperado);

            Thread.Sleep(4000);
            Console.Clear();
        }
        else
        {
            Console.WriteLine($"\nO artista {nomeDoArtista} não foi encontrado!");
            Console.WriteLine("Digite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
