using System.Windows.Input;
using itsRewards.ViewModels.Base;
using itsRewards.Views;
using itsRewards.Views.Auths;
using Xamarin.Forms;

namespace itsRewards.ViewModels
{
    public class SettingsPageViewModel : BaseViewModel
    {
        #region Properties
        #endregion

        #region Commands
        public ICommand ProfileCommand { get; }
        public ICommand NotificationCommand { get; }
        #endregion

        #region Constructor
        public SettingsPageViewModel()
        {
            #region Assign Commands
            ProfileCommand = new Command(ExecuteProfileAsync);
            NotificationCommand = new Command(ExecuteNotifiationAsync);
            #endregion
        }
        #endregion

        #region Open store detail Async
        async void ExecuteProfileAsync()
        {
            App.IsSignUpFirstTime = false;
            await _navigationService.GoToAsync(nameof(SignUpPage).ToLower());
            //await _navigationService.GoToAsync(nameof(StoreDetailPage).ToLower());
        }

        async void ExecuteNotifiationAsync()
        {
            App.IsSignUpFirstTime = false;
            await _navigationService.GoToAsync(nameof(NotificationPage).ToLower());
            //await _navigationService.GoToAsync(nameof(StoreDetailPage).ToLower());
        }

        #endregion
    }
}