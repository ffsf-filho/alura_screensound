using ScreenSound.Web.Response;
using System.Net.Http.Json;

namespace ScreenSound.Web.Services;

public class GeneroAPI(IHttpClientFactory factory)
{
    private readonly HttpClient _httpClient = factory.CreateClient("API");

    public async Task<List<GeneroResponse>?> GetGenerosAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<GeneroResponse>>("generos");
    }
    public async Task<GeneroResponse?> GetGeneroPorNomeAsync(string nome)
    {
        return await _httpClient.GetFromJsonAsync<GeneroResponse>($"generos/{nome}");
    }
}