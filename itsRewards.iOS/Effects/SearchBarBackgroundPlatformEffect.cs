using System;
using itsRewards.iOS.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("itsRewards")]
[assembly: ExportEffect(typeof(SearchBarBackgroundPlatformEffect), "SearchBarBackgroundEffect")]
namespace itsRewards.iOS.Effects
{

    public class SearchBarBackgroundPlatformEffect : PlatformEffect
    {
        private UISearchBar NativeSearchBar => (UISearchBar)Control;
        private SearchBar XamarinSearchBar => (SearchBar)Element;

        protected override void OnAttached()
        {
            //if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            //NativeSearchBar.SearchTextField.BackgroundColor = XamarinSearchBar.BackgroundColor.ToUIColor();
            NativeSearchBar.SearchBarStyle = UISearchBarStyle.Minimal;
            NativeSearchBar.BarTintColor = UIColor.FromRGBA(236,242,250,1);
        }

        protected override void OnDetached()
        {
            // Intentionally left blank.
        }
    }
}

