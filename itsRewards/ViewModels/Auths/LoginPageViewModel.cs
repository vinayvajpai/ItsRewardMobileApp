using System;
using System.Windows.Input;
using itsRewards.Extensions;
using itsRewards.Extensions.Validations;
using itsRewards.Extensions.Validations.Rules;
using itsRewards.Models;
using itsRewards.Models.Auths;
using itsRewards.Services.Altrias;
using itsRewards.Services.Auths;
using itsRewards.Services.Base.ServiceProvider;
using itsRewards.ViewModels.Base;
using itsRewards.Views.Auths;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace itsRewards.ViewModels.Auths
{
    public class LoginPageViewModel : BaseViewModel
    {
        #region Properties
        ValidatableObject<string> _email;
        public ValidatableObject<string> Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        ValidatableObject<string> _password;
        public ValidatableObject<string> Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

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
        public ICommand ValidateEmailCommand { get; }
        public ICommand ValidatePasswordCommand { get; }
        public ICommand ForgotPasswordCommand { get; }
        public ICommand SignUpCommand { get; set; }
        #endregion

        #region Services
        public IAccountService AccountService { get; }
        public IAltriaService AltriaService { get; }
        #endregion

        #region Constructor
        public LoginPageViewModel()
        {
            #region Assign Commands
            ValidateEmailCommand = new Command(() => { ValidateEmail(); });
            ValidatePasswordCommand = new Command(() => { ValidatePassword(); });
            ForgotPasswordCommand = new Command(ExecuteForgotPasswordCommand);

            LoginInCommand = new Command(ExecuteLoginInCommand);
            SignUpCommand = new Command(ExecuteSignUpCommand);
            #endregion

            #region Assign Services
            AccountService = itsRewards.Views.AppShell.Resolve<IAccountService>();
            AltriaService = itsRewards.Views.AppShell.Resolve<IAltriaService>();
            #endregion

            #region Assign ValidationObject
            AddValidations();
            #endregion

#if DEBUG
            Email.Value = "9662513544";
            Password.Value = "@bhishekGosw@mi";

#endif

        }
        #endregion

        #region Validation
        private void AddValidations()
        {
            _email = new ValidatableObject<string>();
            _email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please enter email." });
           // _email.Validations.Add(new EmailRule<string> { ValidationMessage = "Please enter valid email." });

            _password = new ValidatableObject<string>();
            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please enter password." });
        }

        private bool ValidateEmail()
        {
            return _email.Validate();
        }

        private bool ValidatePassword()
        {
            return _password.Validate();
        }

        private bool ValidateData()
        {
            var validateEmail = ValidateEmail();
            var validatePassword = ValidatePassword();
            return validateEmail && validatePassword;

        }
        #endregion


        #region Login
        async void ExecuteLoginInCommand()
        {
            if (ValidateData())
            {
                if (Xamarin.Essentials.Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    AlertMessage.Show("No Internet", "Please check your internet connection", "Ok");
                    return;
                }

                if (IsBusy)
                    return;

                IsBusy = true;
                
                try
                {
                    var response = await AccountService.Login(Email.Value, Password.Value);
                    if (response != null && !string.IsNullOrEmpty(response.Email))
                    {
                        //if (string.IsNullOrEmpty(SharedPreferences.AltriaAccessToken))
                        {
                            var tokenresponse = await AltriaService.AltriaToken();
                            //if (tokenresponse == null || string.IsNullOrEmpty(tokenresponse.AccessToken))
                                //return;
                            SharedPreferences.AltriaAccessToken = tokenresponse.AccessToken;
                            SharedPreferences.LoginUserInfo = JsonConvert.SerializeObject(response);
                        }
                        if (response.Token != null)
                        {
                            SharedPreferences.AuthToken = response.Token[0];
                        }
                        await _navigationService.GoToAsync<HomePageViewModel>("//main", vm => {
                            vm.Stores = response.Stores;
                        });
                    }
                    else
                    {
                        if (response != null && !string.IsNullOrWhiteSpace(response.PortalMessage))
                            AlertMessage.Show("Invalid Login", response.PortalMessage, "OK");
                        else
                            AlertMessage.Show("Server Error!", "Something went wrong", "Ok");
                    }
                }
                catch (HttpRequestExceptionEx ex)
                {
                    if (ex.HttpCode == System.Net.HttpStatusCode.PreconditionFailed)
                    {
                        AlertMessage.Show("Server Error!", "Username or Password are incorrect.", "Ok");
                    }
                    else
                    {
                        AlertMessage.Show("Server Error!", ex.Message, "Ok");
                    }
                }
                catch (Exception ex)
                {
                    AlertMessage.Show("Server Error!", ex.Message, "Ok");
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }
        #endregion

        #region Forgot Password
        async void ExecuteForgotPasswordCommand()
        {
            //await _navigationService.GoToAsync($"{nameof(ForgotPasswordPage).ToLower()}?email={Email.Value}");
        }
        #endregion

        #region SignUp
        async void ExecuteSignUpCommand()
        {
            App.IsSignUpFirstTime = true;
            await _navigationService.GoToAsync(nameof(SignUpPage).ToLower());
        }
        #endregion
    }
}