using ScreenSound.Banco;
using ScreenSound.Menus;
using ScreenSound.Modelos;

try
{
    ScreenSoundContext context = new();
    ArtistaDAL artistaDAL = new(context);

    //Artista novoArtista = new("Foo Fighters", "Foo Fighters é uma banda de rock alternativo americana formada por Dave Grohl em 1995.");
    //Artista novoArtista = new("Gilberto Gil", "Gilberto Passos Gil Moreira é um cantor, compositor, multi - instrumentista, produtor musical, político e escritor brasileiro.");
    //Artista novoArtista = new("Tim Maia", "Excelente cantor.");
    //artistaDAL.Adicionar(novoArtista);

    //Artista alteraArtista = new("Gilberto Gil", "Gilberto Passos Gil Moreira é um cantor, compositor, multi - instrumentista.") { 
    //    Id = 2
    //};
    //artistaDAL.Atualizar(alteraArtista);

    //Artista apagaArtista = new("Tim Maia", "Excelente cantor.") { Id = 3};
    //artistaDAL.Deletar(apagaArtista);

    foreach (Artista artista in artistaDAL.Listar())
    {
        Console.WriteLine(artista);
    }

    Console.WriteLine("=================================================");

    Console.WriteLine(artistaDAL.RecuperarPeloNome("Djavan"));
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

return;

Artista ira = new("Ira!", "Banda Ira!");
Artista beatles = new("The Beatles", "Banda The Beatles");

Dictionary<string, Artista> artistasRegistrados = new()
{
    { ira.Nome, ira },
    { beatles.Nome, beatles }
};

Dictionary<int, Menu> opcoes = new()
{
    { 1, new MenuRegistrarArtista() },
    { 2, new MenuRegistrarMusica() },
    { 3, new MenuMostrarArtistas() },
    { 4, new MenuMostrarMusicas() },
    { -1, new MenuSair() }
};

void ExibirLogo()
{
    Console.WriteLine(@"

░██████╗░█████╗░██████╗░███████╗███████╗███╗░░██╗  ░██████╗░█████╗░██╗░░░██╗███╗░░██╗██████╗░
██╔════╝██╔══██╗██╔══██╗██╔════╝██╔════╝████╗░██║  ██╔════╝██╔══██╗██║░░░██║████╗░██║██╔══██╗
╚█████╗░██║░░╚═╝██████╔╝█████╗░░█████╗░░██╔██╗██║  ╚█████╗░██║░░██║██║░░░██║██╔██╗██║██║░░██║
░╚═══██╗██║░░██╗██╔══██╗██╔══╝░░██╔══╝░░██║╚████║  ░╚═══██╗██║░░██║██║░░░██║██║╚████║██║░░██║
██████╔╝╚█████╔╝██║░░██║███████╗███████╗██║░╚███║  ██████╔╝╚█████╔╝╚██████╔╝██║░╚███║██████╔╝
╚═════╝░░╚════╝░╚═╝░░╚═╝╚══════╝╚══════╝╚═╝░░╚══╝  ╚═════╝░░╚════╝░░╚═════╝░╚═╝░░╚══╝╚═════╝░
");
    Console.WriteLine("Boas vindas ao Screen Sound 3.0!");
}

void ExibirOpcoesDoMenu()
{
    ExibirLogo();
    Console.WriteLine("\nDigite 1 para registrar um artista");
    Console.WriteLine("Digite 2 para registrar a música de um artista");
    Console.WriteLine("Digite 3 para mostrar todos os artistas");
    Console.WriteLine("Digite 4 para exibir todas as músicas de um artista");
    Console.WriteLine("Digite -1 para sair");

    Console.Write("\nDigite a sua opção: ");
    string opcaoEscolhida = Console.ReadLine()!;
    int opcaoEscolhidaNumerica = int.Parse(opcaoEscolhida);

    if (opcoes.ContainsKey(opcaoEscolhidaNumerica))
    {
        Menu menuASerExibido = opcoes[opcaoEscolhidaNumerica];
        menuASerExibido.Executar(artistasRegistrados);
        if (opcaoEscolhidaNumerica > 0) ExibirOpcoesDoMenu();
    }
    else
    {
        Console.WriteLine("Opção inválida");
    }
}

ExibirOpcoesDoMenu();