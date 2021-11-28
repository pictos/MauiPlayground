using MauiApp8.ViewModel;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp8.Views;
public partial class ComicPage : ContentPage
{
	public ComicPage()
	{
		InitializeComponent();
		img ??= new Image();
		btn ??= new Button();
		CustomizeButton();
	}

	void CustomizeButton()
	{
		Button.ControlsViewMapper.AppendToMapping(nameof(IButton.Clicked), (h, v) =>
		{
			if (v is not Button button)
				return;
			button.BackgroundColor = Colors.Red;
		});
		Button.ControlsViewMapper.AppendToMapping(nameof(IButton.Released), (h, v) =>
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
			};
		}


	}
}
