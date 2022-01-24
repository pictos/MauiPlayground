using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific;
using System.Diagnostics;
using System.Threading.Tasks;
using Application = Microsoft.Maui.Controls.Application;

namespace MauiApp8
{
	public partial class App : Application
	{
		internal static IMauiContext MauiContext => App.Current.Handler.MauiContext;

		public App()
		{
			InitializeComponent();
			//MainPage = new AppShell();
			MainPage = new MainPage();
			DoSomethingAsync();
		}

		protected override void OnStart()
		{
			DoSomethingAsync();
			var x = new Microsoft.Maui.Controls.Label();
			var z = x is ITextStyle;
		}


		public async Task DoSomethingAsync()
		{
			Debug.WriteLine("Something");
			await Task.Delay(1000);
			Debug.WriteLine("Something");
			await Task.Delay(1000);
			Debug.WriteLine("Something");
			await Task.Delay(1000);
			Debug.WriteLine("Something");
			await Task.Delay(2000);
		}
	}
}
