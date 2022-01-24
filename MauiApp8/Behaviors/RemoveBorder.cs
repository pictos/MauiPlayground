using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp8.Behaviors;

sealed partial class RemoveBorder : Behavior<View>
{
	protected override void OnAttachedTo(View bindable)
	{
		base.OnAttachedTo(bindable);
		OnPlatformAttached(bindable);
	}

	protected override void OnDetachingFrom(View bindable)
	{
		base.OnDetachingFrom(bindable);
		OnPlatformDetached(bindable);
	}

	public partial void OnPlatformAttached(View view);

	public partial void OnPlatformDetached(View view);
}
