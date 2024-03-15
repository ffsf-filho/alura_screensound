namespace ScreenSound.Modelos; 

public class Artista(string nome, string bio)
{
    public virtual ICollection<Musica> Musicas { get; set; } = new List<Musica>();

    public string Nome { get; set; } = nome;
    public string FotoPerfil { get; set; } = "https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_1280.png";
    public string Bio { get; set; } = bio;
    public int Id { get; set; }

    public void AdicionarMusica(Musica musica)
    {
        Musicas.Add(musica);
    }

    public void ExibirDiscografia()
    {
        Console.WriteLine($"Discografia do artista {Nome}");
        foreach (var musica in Musicas)
        {
            Console.WriteLine($"Música: {musica.Nome} - Ano de Lançamento: {musica.AnoLancamento}");
        }
    }

    public override string ToString()
    {
        return $@"Id: {Id}
            Nome: {Nome}
            Foto de Perfil: {FotoPerfil}
            Bio: {Bio}";
    }
}