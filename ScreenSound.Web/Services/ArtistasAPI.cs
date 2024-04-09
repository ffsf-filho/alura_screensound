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
}
