namespace ScreenSound.Shared.Modelos.Modelos;

public class Artista(string nome, string bio, string? fotoPerfil)
{
    public virtual ICollection<Musica> Musicas { get; set; } = [];

    public string Nome { get; set; } = nome;
    public string FotoPerfil { get; set; } = !String.IsNullOrWhiteSpace(fotoPerfil)? $"/FotoPerfil/{fotoPerfil}" : "https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_1280.png";
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