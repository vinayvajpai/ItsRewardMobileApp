using System.Windows.Input;
using itsRewards.ViewModels.Base;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace itsRewards.ViewModels
{
    public class LandingPageViewModel : BaseViewModel
    {
        #region Properties
        public string VersionString
        {
            get
            {
                return "Version " + AppInfo.VersionString;
            }
        }
        #endregion

        #region Commands
        public ICommand LoginInCommand { get; set; }
        public ICommand SignUpCommand { get; set; }
        #endregion

        #region Constructor
        public LandingPageViewModel()
        {
            #region Assign Commands
            LoginInCommand = new Command(ExecuteLoginInCommand);
            SignUpCommand = new Command(ExecuteSignUpCommand);
            #endregion
        }
        #endregion

        #region Login
        async void ExecuteLoginInCommand()
        {
            await _navigationService.GoToAsync("loginpage");
        }
        #endregion

        #region SignUp
        async void ExecuteSignUpCommand()
        {
            App.IsSignUpFirstTime = true;
            await _navigationService.GoToAsync("signuppage");
        }
        #endregion
    }
}