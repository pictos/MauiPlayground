namespace MauiApp8;

sealed class Constants
{
	public const string BaseUrl = "https://xkcd.com";
	public const string LatestComic = "/info.0.json";
	public const string ComicById = "/{0}" + LatestComic;
}
