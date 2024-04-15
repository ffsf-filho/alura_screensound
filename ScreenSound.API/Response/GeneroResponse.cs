namespace ScreenSound.API.Response;

public record GeneroResponse(int Id, string Nome, string Descricao)
{
    public override string ToString()
    {
        return $"{this.Nome}";
    }
}
