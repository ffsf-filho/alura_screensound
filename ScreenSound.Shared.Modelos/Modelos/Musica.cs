namespace ScreenSound.Shared.Modelos.Modelos;

public class Musica(string nome)
{
    public string Nome { get; set; } = nome;
    public int Id { get; set; }
    public int? AnoLancamento { get; set; }
    public int? ArtistaId { get; set; }
    public virtual Artista? Artista { get; set; }
    public virtual ICollection<Genero> Generos { get; set; } = [];

    public void ExibirFichaTecnica()
    {
        Console.WriteLine($"Nome: {Nome}");
      
    }

    public override string ToString()
    {
        return @$"Id: {Id}
        Nome: {Nome}";
    }
}