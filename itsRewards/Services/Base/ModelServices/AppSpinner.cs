using System;
using System.Threading.Tasks;
using itsRewards.Views.CommonViews;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace itsRewards.Services.Base.ModelServices
{
    public class AppSpinner : ISpinner, IDisposable
    {
        private SpinnerPage _spinnerPageInstance;
        private bool _isSpinning;

        public AppSpinner() { }

        public void HideLoading()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                int retry = 0;

                if (_spinnerPageInstance == null)
                    return;

                while (!_isSpinning && retry <= 5)
                {
                    await Task.Delay(200);
                    retry++;
                }

                if (_spinnerPageInstance != null)
                    await App.Current.MainPage.Navigation.RemovePopupPageAsync(_spinnerPageInstance, true);

                _spinnerPageInstance = null;
                _isSpinning = false;

            });
        }

        public void ShowLoading(bool isCancellable = true)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (_spinnerPageInstance == null)
                {
                    _spinnerPageInstance = new SpinnerPage(isCancellable);

                    await App.Current.MainPage.Navigation.PushPopupAsync(_spinnerPageInstance, true)
                        .ContinueWith((t) => _isSpinning = true);
                }
            });
        }

        public void Dispose() { }
    }
}