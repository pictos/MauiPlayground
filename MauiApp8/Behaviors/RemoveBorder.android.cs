using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using AView = Android.Views.View;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp8.Behaviors;

sealed partial class RemoveBorder
{
	Drawable? originalBackground;
	public async partial void OnPlatformAttached(View view) 
	{
		await Task.Delay(5000);
		var nativeControl = view.ToNative(App.MauiContext);
		originalBackground = nativeControl.Background;

		var shape = new ShapeDrawable(new RectShape());

		if (shape.Paint is not null)
		{
			shape.Paint.Color = global::Android.Graphics.Color.Transparent;
			shape.Paint.StrokeWidth = 0;
			shape.Paint.SetStyle(Paint.Style.Stroke);
		}

		nativeControl.Background = shape;
	}
	public partial void OnPlatformDetached(View view)
	{
		var nativeControl = view.Background.ToNative(view.Handler.MauiContext!);
		
		if (nativeControl is null)
			return; 

		nativeControl.Background = originalBackground;
	}
}
