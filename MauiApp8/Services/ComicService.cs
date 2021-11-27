using MauiApp8.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using static MauiApp8.Constants;

namespace MauiApp8.Services;

internal sealed class ComicService
{
	readonly HttpClient httpClient;
	const string acceptedMediaType = "application/json";
	public ComicService()
	{
		httpClient = new HttpClient
		{
			BaseAddress = new Uri(BaseUrl)
		};
		httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(acceptedMediaType));
	}

	public Task<Comic?> GetLatestCommicAsync() =>
		GetCommitAsync(LatestComic);

	public Task<Comic?> GetCommicById(uint id)
	{
		var url = string.Format(ComicById, id);
		return GetCommitAsync(url);
	}

	async Task<Comic?> GetCommitAsync(string url)
	{
		var response = await httpClient.GetAsync(url).ConfigureAwait(false);

		if (!response.IsSuccessStatusCode)
			throw new Exception("Algo de errado não está certo.");

		var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
		var model = DeserializeJsonFromStream<Comic>(stream);

		return model;
	}

	static readonly JsonSerializer js = new();

	static TModel? DeserializeJsonFromStream<TModel>(Stream stream)
	{
		if (stream is null || !stream.CanRead)
			return default;

		using var sr = new StreamReader(stream);
		using var jtr = new JsonTextReader(sr);

		var result = js.Deserialize<TModel>(jtr);

		return result;
	}

	static async ValueTask<string> StreamToStringAsync(Stream stream)
	{
		var content = string.Empty;

		if (stream is null || !stream.CanRead)
			return content;

		using var sr = new StreamReader(stream);
		content = await sr.ReadToEndAsync().ConfigureAwait(false);

		return content;
	}
}
