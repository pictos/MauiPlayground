using MauiApp8.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiApp8.ViewModel;

abstract class BaseViewModel
{
	protected ComicService ApiService { get; }

	bool isBusy = false;

	public bool IsBusy
	{
		get => isBusy;
		set => SetProperty(ref isBusy, value);
	}

	string title = string.Empty;
	public string Title
	{
		get => title;
		set => SetProperty(ref title, value);
	}

	public BaseViewModel()
	{
		ApiService = ServiceProvider.GetService<ComicService>() ?? throw new NullReferenceException(nameof(ComicService));
	}

	protected bool SetProperty<T>(ref T backingStore, T value,
		[CallerMemberName] string propertyName = "")
	{
		if (EqualityComparer<T>.Default.Equals(backingStore, value))
			return false;

		backingStore = value;
		OnPropertyChanged(propertyName);
		return true;
	}

	public event PropertyChangedEventHandler? PropertyChanged;
	protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
	{
		var changed = PropertyChanged;

		changed?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
