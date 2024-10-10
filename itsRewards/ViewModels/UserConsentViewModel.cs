using System;
using System.Windows.Input;
using itsRewards.ViewModels.Base;
using itsRewards.Views;
using Xamarin.Forms;

namespace itsRewards.ViewModels
{
	public class UserConsentViewModel : BaseViewModel
    {
		public UserConsentViewModel()
		{
            SubmitCommand = new Command(ExecuteSubmitCommand);
        }

        #region Commands
        public ICommand SubmitCommand { get; }
        #endregion

        #region Methods
       async void ExecuteSubmitCommand()
        {
            try
            {
                await _navigationService.GoToAsync(nameof(VerifyInfoPage).ToLower());
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}

