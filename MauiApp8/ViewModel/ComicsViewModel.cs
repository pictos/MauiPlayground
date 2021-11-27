using MauiApp8.Models;
using Microsoft.Maui.Controls;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;

namespace MauiApp8.ViewModel;

sealed class ComicsViewModel : BaseViewModel
{
	private string? comicUrl;

	public string? ComicUrl
	{
		get => comicUrl;
		set => SetProperty(ref comicUrl, value);
	}

	int maxNumber = -1;

	public AsyncRelayCommand ComicCommand { get; }

	public ComicsViewModel()
	{
		ComicCommand = new AsyncRelayCommand(ComicCommandExecute);
		ComicCommand.Execute(null);
	}

	async Task ComicCommandExecute()
	{
		if (IsBusy)
			return;

		IsBusy = true;

		try
		{
			Comic? comic;

			if (maxNumber is -1)
			{
				comic = await ApiService.GetLatestCommicAsync().ConfigureAwait(false);

				if (await IsComicValid(comic))
					maxNumber = comic!.Num;
			}
			else
			{
				comic = await ApiService.GetCommicById(ComicId(maxNumber)).ConfigureAwait(false);
				await IsComicValid(comic);
			}

			ComicUrl = comic!.Img?.ToString();
		}
		finally
		{
			IsBusy = false;
		}

		static async ValueTask<bool> IsComicValid(Comic? comic)
		{
			if (comic is null)
			{
				await Shell.Current.DisplayAlert("Erro", "Não foi possível carregar tirinha", "Ok");
				return false;
			}

			return true;
		}

		static uint ComicId(in int maxValue)
		{
			return (uint)Random.Shared.Next(0, maxValue);
		}
	}
}
