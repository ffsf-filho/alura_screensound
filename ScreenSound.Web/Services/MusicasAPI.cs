﻿using ScreenSound.Web.Response;
using System.Net.Http.Json;

namespace ScreenSound.Web.Services;

public class MusicasAPI(IHttpClientFactory factory)
{
	private readonly HttpClient _httpClient = factory.CreateClient("API");

	public async Task<ICollection<MusicaResponse>?> GetMusicasAsync()
	{
		return await _httpClient.GetFromJsonAsync<ICollection<MusicaResponse>>("Musicas");
	}
}
