using ScreenSound.Web.Requests;
using ScreenSound.Web.Response;
using System.Net.Http.Json;

namespace ScreenSound.Web.Services;

public class ArtistasAPI(IHttpClientFactory factory)
{
	private readonly HttpClient _httpClient = factory.CreateClient("API");

	public async Task<ICollection<ArtistaResponse>?> GetArtistasAsync()
	{
		return await _httpClient.GetFromJsonAsync<ICollection<ArtistaResponse>>("Artistas");
	}

	public async Task AddArtistaAsync(ArtistaRequest artista)
	{
		await _httpClient.PostAsJsonAsync("Artistas", artista);
	}

	public async Task DeleteArtistaAsync(int artistaId)
	{
		await _httpClient.DeleteAsync($"Artistas/{artistaId}");
	}

	public async Task UpdateArtistaAsync(ArtistaResponse artista)
	{
		await _httpClient.PutAsJsonAsync("Artistas", artista);
	}

	public async Task<ArtistaResponse?> GetArtistaPorNomeAsync(string nomeArtista)
	{
		return await _httpClient.GetFromJsonAsync<ArtistaResponse>($"Artistas/{nomeArtista}");
	}
}
