/*
 * 
 */
using System;
using Xamarin.Forms;

namespace itsRewards.Views
{
    public partial class OfferDetailPage : ContentPage
    {
        private string _orgSource;
        public OfferDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            _orgSource = string.Empty;
            base.OnAppearing();
            ViewModel.LoadDataCommand.Execute(null);
           //.Source.GetValue(null) as string;
        }

        protected override void OnDisappearing()
        {
            ViewModel.CouponHtml = "about:page";
            webView.Reload();
            base.OnDisappearing();
        }

        public void OnForwardButtonClicked(object sender, EventArgs e)
        {
            if (webView.CanGoForward)
            {
                webView.GoForward();
            }
        }

        public async void OnBackButtonClicked(System.Object sender, System.EventArgs e)
        {
             
            if (!_orgSource.Contains("login") && webView.CanGoBack)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    webView.GoBack();
                });
            }
            else
            {
                await Navigation.PopAsync();
            }

        }

        private void webView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            _orgSource = e.Url;
        }
    }
}