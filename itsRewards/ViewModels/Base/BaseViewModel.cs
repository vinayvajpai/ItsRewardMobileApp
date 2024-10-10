using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Threading.Tasks;
using itsRewards.Services.Base.NavigationServices;
using itsRewards.Services.Base.ModelServices;
using System.Windows.Input;
using itsRewards.Extensions;
using itsRewards.Services.Base;
using itsRewards.Views;

namespace itsRewards.ViewModels.Base
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                SetProperty(ref isBusy, value);
                if (IsBusy)
                {
                    _aapSpinner.ShowLoading(false);
                }
                else
                {
                    _aapSpinner.HideLoading();
                }
            }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        protected INavigationService _navigationService => itsRewards.Views.AppShell.Resolve<INavigationService>();
        protected ISpinner _aapSpinner => itsRewards.Views.AppShell.Resolve<ISpinner>();
        protected IInitialiserService _initialiserService => itsRewards.Views.AppShell.Resolve<IInitialiserService>();


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Services
        public IAlertMessage AlertMessage { get; }
        #endregion

        #region Command
        public ICommand SettingsCommand { get; }
        public ICommand BackCommand { get; }
        public ICommand LogoutCommand { get; set; }
        #endregion

        public BaseViewModel()
        {
            AlertMessage = itsRewards.Views.AppShell.Resolve<IAlertMessage>();
            #region Command
            BackCommand = new Command(ExecuteBackCommand);
            LogoutCommand = new Command(ExecuteLogoutCommand);
            SettingsCommand = new Command(ExecuteSettingsCommand);
            #endregion
            _initialiserService.Initialise(this);
        }

        protected virtual async Task GoBack()
        {
            await _navigationService.GoBackAsync();
        }

        async void ExecuteBackCommand()
        {
            if (Shell.Current != null)
            {
                Shell.Current.FlyoutIsPresented = false;
                await GoBack();
            }
        }
        
        async void ExecuteLogoutCommand()
        {
            SharedPreferences.AuthToken = string.Empty;
            SharedPreferences.LoginUserInfo = string.Empty;
            await _navigationService.GoToAsync("///landing");
        }

        async void ExecuteSettingsCommand()
        {
            await _navigationService.GoToAsync(nameof(SettingsPage).ToLower());
        }
    }
}
