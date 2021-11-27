using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using System;

namespace MauiApp8.Services;

internal static class ServiceProvider
{
	public static IServiceProvider? Current =>
#if WINDOWS10_0_17763_0_OR_GREATER
			MauiWinUIApplication.Current.Services;
#elif ANDROID
			MauiApplication.Current.Services;
#elif IOS || MACCATALYST
			MauiUIApplicationDelegate.Current.Services;
#else
			null;
#endif

	public static TService? GetService<TService>() => Current!.GetService<TService>();
}
