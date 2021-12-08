using MauiApp8.ViewModel;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace MauiApp8.Views;
public partial class ComicPage : ContentPage
{
	public ComicPage()
	{
		InitializeComponent();
		img ??= new();
		btn ??= new();
		indicator ??= new();
		CustomizeButton();

		btn.Layout(new Rectangle());
	}

	void CustomizeButton()
	{
		Button.ControlsViewMapper.AppendToMapping(nameof(IButton.Clicked), (h, v) =>
		{
			if (v is not Button button)
				return;
			button.BackgroundColor = Colors.Red;
		});
		Button.ControlsViewMapper
			.AppendToMapping(nameof(IButton.Released), (h, v) =>
		{
			if (v is not Button button)
				return;
			button.BackgroundColor = Colors.Blue;
		});
	}

	protected override void OnAppearing()
	{
		if (BindingContext is ComicsViewModel vm)
		{
			vm.PropertyChanged += (_, e) =>
			{
				if (e.PropertyName == nameof(ComicsViewModel.ComicUrl))
					Device.BeginInvokeOnMainThread(() => img.Source = vm.ComicUrl);
				else if (e.PropertyName == nameof(BaseViewModel.IsBusy))
					Device.BeginInvokeOnMainThread(() =>
						indicator.IsVisible = indicator.IsRunning = vm.IsBusy);
			};
		}
	}
}
