using System;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using itsRewards.Views.CommonViews;

namespace itsRewards.Services.Base.ModelServices
{
    public class AlertMessage : IAlertMessage
    {
        private AlertPage _alertPageInstance;
        private bool _isOpened = false;

        public void Show(string title, string message, string cancelButtonName, bool isCancellable = true)
        {
            if (!_isOpened)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (_alertPageInstance == null)
                    {
                        _alertPageInstance = new AlertPage(isCancellable);
                        _alertPageInstance.CloseButtonClicked += _alertPageInstance_CloseButtonClicked;
                    }
                    _alertPageInstance.AlertTitle = title;
                    _alertPageInstance.AlertMessage = message;
                    _alertPageInstance.CancelButtonText = cancelButtonName;
                    _alertPageInstance.CloseWhenBackgroundIsClicked = false;
                    _alertPageInstance.backgroundClick += _alertPageInstance_backgroundClick;
                    await App.Current.MainPage.Navigation.PushPopupAsync(_alertPageInstance, true)
                    .ContinueWith((t) => _isOpened = true);
                });
            }
        }

        private void _alertPageInstance_backgroundClick(object sender, EventArgs e)
        {
            _isOpened = false;
        }

        private async void _alertPageInstance_CloseButtonClicked(object sender, EventArgs e)
        {
            if (_alertPageInstance != null && _isOpened)
            {
                await App.Current.MainPage.Navigation.RemovePopupPageAsync(_alertPageInstance, true)
                .ContinueWith((t) => _isOpened = false);
            }
        }
    }
}