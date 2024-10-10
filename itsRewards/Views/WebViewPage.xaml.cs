using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using itsRewards.Helpers;
using itsRewards.ViewModels;
using itsRewards.ViewModels.Base;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace itsRewards.Views
{	
	public partial class WebViewPage : ContentPage
	{
        BaseViewModel vm;
		public WebViewPage ()
		{
			InitializeComponent ();
            BindingContext = vm = new BaseViewModel();
			webView.Source = new HtmlWebViewSource()
			{
				Html = UriHelper.ReadHtmlFileContent("test.html")
			};
            webView.Navigating += WebView_Navigating;
            webView.Navigated += WebView_Navigated;
            NavigationPage.SetIconColor(this, Color.FromHex("#000000"));
            if (HomePageViewModel.SelectedStores != null)
            {
                lblTitle.Text = HomePageViewModel.SelectedStores.StoreName;
            }
        }

        private void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            vm.IsBusy = false;
        }

        private void WebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            vm.IsBusy = true;
        }
    }
}

