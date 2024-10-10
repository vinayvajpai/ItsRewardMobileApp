using System;
using System.Collections.Generic;
using System.Windows.Input;
using itsRewards.Extensions;
using itsRewards.Extensions.Validations;
using itsRewards.Extensions.Validations.Rules;
using itsRewards.Helpers;
using itsRewards.Models;
using itsRewards.Models.Auths;
using itsRewards.Services.Auths;
using itsRewards.Services.Base.ServiceProvider;
using itsRewards.ViewModels.Base;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace itsRewards.ViewModels.Auths
{
    public class SignUpPageViewModel : BaseViewModel
    {
        #region Properties
        ValidatableObject<string> _username;
        public ValidatableObject<string> UserName
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        ValidatableObject<string> _email;
        public ValidatableObject<string> Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        ValidatableObject<string> _cell;
        public ValidatableObject<string> Cell
        {
            get { return _cell; }
            set { SetProperty(ref _cell, value); }
        }

        ValidatableObject<string> _password;
        public ValidatableObject<string> Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        ValidatableObject<string> _confirmPassword;
        public ValidatableObject<string> ConfirmPassword
        {
            get { return _confirmPassword; }
            set { SetProperty(ref _confirmPassword, value); }
        }

        string _address1;
        public string Address1
        {
            get => _address1;
            set => SetProperty(ref _address1, value);
        }

        string _address2;
        public string Address2
        {
            get => _address2;
            set => SetProperty(ref _address2, value);
        }

        string _address3;
        public string Address3
        {
            get => _address3;
            set => SetProperty(ref _address3, value);
        }

        string _city;
        public string City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }

        string _state;
        public string State
        {
            get => _state;
            set => SetProperty(ref _state, value);
        }
        string _zip;
        public string Zip
        {
            get => _zip;
            set => SetProperty(ref _zip, value);
        }

        bool _hasNotRegistered;
        public bool HasNotRegistered
        {
            get => _hasNotRegistered;
            set => SetProperty(ref _hasNotRegistered, value);
        }

        public string VersionString
        {
            get
            {
                return "Version " + AppInfo.VersionString;
            }
        }

        private List<string> _stateList;
        public List<string> StateList
        {
            get => _stateList;
            set => SetProperty(ref _stateList, value);
        }

        string _tobaccoMember;
        public string TobaccoMember
        {
            get => _tobaccoMember;
            set => SetProperty(ref _tobaccoMember, value);
        }

        bool _cigar;
        public bool Cigar
        {
            get => _cigar;
            set => SetProperty(ref _cigar, value);
        }

        bool _mst;
        public bool Mst
        {
            get => _mst;
            set => SetProperty(ref _mst, value);
        }

        bool _otdn;
        public bool OTDN
        {
            get => _otdn;
            set => SetProperty(ref _otdn, value);
        }

        bool _cigarette;
        public bool Cigarette
        {
            get => _cigarette;
            set => SetProperty(ref _cigarette, value);
        }

        bool _isFirstSignUpFinished;
        public bool IsFirstSignUpFinished
        {
            get => _isFirstSignUpFinished;
            set => SetProperty(ref _isFirstSignUpFinished, value);
        }

        bool _signUpVisible;
        public bool SignUpVisible
        {
            get => _signUpVisible;
            set => SetProperty(ref _signUpVisible, value);
        }

        string _pin;
        public string Pin
        {
            get => _pin;
            set => SetProperty(ref _pin, value);
        }

        private bool isAgree = false;
        public bool IsAgree
        {
            get => isAgree;
            set => SetProperty(ref isAgree, value);
        }

        #endregion

        #region Commands
        public ICommand LoginInCommand { get; set; }
        public ICommand ValidateUsernameCommand { get; }
        public ICommand ValidateCellCommand { get; }
        public ICommand ValidateEmailCommand { get; }
        public ICommand ValidatePasswordCommand { get; }
        public ICommand SignUpCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand PinCommand { get; set; }
        public ICommand ResendPinCommand { get; set; }
        #endregion

        #region Services
        public IAccountService AccountService { get; }
        #endregion

        #region Constructor
        public SignUpPageViewModel()
        {
            #region Assign Commands
            ValidateUsernameCommand = new Command(() => { ValidateUsername(); });
            ValidateEmailCommand = new Command(() => { ValidateEmail(); });
            ValidatePasswordCommand = new Command(() => { ValidatePassword(); });
            ValidateCellCommand = new Command(() => { ValidateCell(); });

            LoginInCommand = new Command(ExecuteLogInCommand);
            SignUpCommand = new Command(ExecuteSignUpCommand);
            CancelCommand = new Command(ExecuteCancelCommand);
            PinCommand = new Command(ExecutePinCommand);
            ResendPinCommand = new Command(ExecuteResendPinCommand);
            #endregion

            #region Assign Services
            AccountService = itsRewards.Views.AppShell.Resolve<IAccountService>();
            #endregion

            #region Assign ValidationObject
            AddValidations();
            BindState();
            SignUpVisible = App.IsSignUpFirstTime;
            HasNotRegistered = !App.IsSignUpFirstTime;
            #endregion
            GetUserInfo();
        }




        #endregion

        #region Validation
        private void AddValidations()
        {
            _username = new ValidatableObject<string>();
            _username.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please enter username." });

            _email = new ValidatableObject<string>();
            _email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please enter email." });
            _email.Validations.Add(new EmailRule<string> { ValidationMessage = "Please enter valid email." });

            _password = new ValidatableObject<string>();
            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please enter password." });
            //_password.Validations.Add(new PasswordRule<string> { ValidationMessage = "Password should be minimum 8 characters long, contains 1 number, 1 capital and small letter and 1 special symbol." });

            _confirmPassword = new ValidatableObject<string>();
            _confirmPassword.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please enter confirm password." });

            _cell = new ValidatableObject<string>();
            _cell.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Please enter cell." });
        }

        private bool ValidateEmail()
        {
            return _email.Validate();
        }

        private bool ValidateUsername()
        {
            return _username.Validate();
        }

        private bool ValidatePassword()
        {
            return _password.Validate();
        }
        private bool ValidateConfirmPassword()
        {
            return _password != _confirmPassword;
        }
        private bool ValidateCell()
        {
            return _cell.Validate();
        }

        private bool ValidateData()
        {
            if (!App.IsSignUpFirstTime) return true;

            // var validateUsername = ValidateUsername();
            var validatePassword = ValidatePassword();
            var validateConfirmPassword = ValidateConfirmPassword();
            var validateEmail = ValidateEmail();
            var validateCell = ValidateCell();
            return validatePassword && validateEmail && validateCell && validateConfirmPassword;
        }
        #endregion


        #region Login
        async void ExecuteSignUpCommand()
        {
            if (!IsAgree)
                return;

            if (ValidateData())
            {
                if (Xamarin.Essentials.Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    AlertMessage.Show("No Internet", "Please check your internet connection", "Ok");
                    return;
                }

                if (!string.IsNullOrWhiteSpace(Password.Value) && Password.Value.Length < 8)
                {
                    AlertMessage.Show("Invalid Password", "Password Should atleast 8 characters long", "Ok");
                    return;
                }
                if (IsBusy)
                    return;

                IsBusy = true;

                try
                {
                    var notificationSetting = UriHelper.GetNotificationSetting();
                    var registerRequest = new RegistrationRequestModel
                    {
                        Email = Email.Value,
                        Password = Password.Value,
                        Username = Cell.Value,
                        Cell = Cell.Value,
                    };
                    if (!App.IsSignUpFirstTime)
                    {
                        registerRequest.Address1 = Address1;
                        registerRequest.Address2 = Address2;
                        registerRequest.Address3 = Address3;
                        registerRequest.AgeVerified = false;
                        registerRequest.City = City;
                        registerRequest.State = State;
                        registerRequest.Zip = Zip;
                        registerRequest.CancelAfterDrive = notificationSetting.AfterNotification;
                        registerRequest.CancelMorningDrive = notificationSetting.BeforeNotification;
                        registerRequest.OptOutEmail = notificationSetting.EmailNotification;
                        registerRequest.OptOutNotifications = notificationSetting.AppNotification;
                        registerRequest.Purchases = new Purchases()
                        {
                            Cigar = Cigar,
                            Cigarette = Cigarette,
                            Mst = Mst,
                            OTDN = OTDN,
                            TobaccoMember = TobaccoMember
                        };

                    }
                    registerRequest.IsRegistered = !App.IsSignUpFirstTime;
                    ApiResponse<string> response = await AccountService.Register(registerRequest);

                    if (response != null && !response.IsError)
                    {
                        IsFirstSignUpFinished = true;
                        if (App.IsSignUpFirstTime)
                        {
                            SignUpVisible = false;
                            Pin = response.Result;
                        }
                        else
                        {
                            await _navigationService.GoToAsync("//main");
                        }
                        IsBusy = false;
                        // 
                    }
                    else
                    {
                        IsFirstSignUpFinished = false;
                        if (App.IsSignUpFirstTime)
                        {
                            SignUpVisible = true;
                        }
                        AlertMessage.Show("Server Error!", response.ResponseException.ExceptionMessage, "Ok");
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

        #region SignUp
        async void ExecuteLogInCommand()
        {
            await _navigationService.GoBackAsync();
        }

        /// <summary>
        /// Cancel and Go back to settings page
        /// </summary>
        async void ExecuteCancelCommand()
        {
            await _navigationService.GoBackAsync();
        }

        public async void ExecutePinCommand(object code)
        {
            if (string.IsNullOrWhiteSpace(Pin))
            {
                AlertMessage.Show("Error", "Please provide 6 digit code", "OK");
                return;
            }

            if (code.ToString() == Pin)
            {
                await AccountService.ConfirmPin(UserName.Value, Password.Value);
                await _navigationService.GoToAsync("//main");
            }
            else
            {
                AlertMessage.Show("Error", "Verification Code doesn't match", "OK");
            }
        }
        private async void ExecuteResendPinCommand(object obj)
        {
            IsBusy = true;
            var response = await AccountService.NewPin(SharedPreferences.AuthToken);
            if (response != null && !response.IsError && !string.IsNullOrWhiteSpace(response.Result))
            {
                Pin = response.Result;
                AlertMessage.Show("Info", "Verification code has been re-sent!", "OK");
            }
            else
            {
                AlertMessage.Show("Error", "Failed to re-send Verification code.", "OK");
            }
            IsBusy = false;
        }
        /// <summary>
        /// Bind static list of States and Number of stores
        /// </summary>
        void BindState()
        {
            // Get static list of States
            StateList = new List<string>()
            {   "AL",
                "AK",
                "AS",
                "AZ",
                "AR",
                "CA",
                "CO",
                "CT",
                "DE",
                "DC",
                "FL",
                "GA",
                "GU",
                "HI",
                "ID",
                "IL",
                "IN",
                "IA",
                "KS",
                "KY",
                "LA",
                "ME",
                "MD",
                "MA",
                "MI",
                "MN",
                "MS",
                "MO",
                "MT",
                "NE",
                "NV",
                "NH",
                "NJ",
                "NM",
                "NY",
                "NC",
                "ND",
                "MP",
                "OH",
                "OK",
                "OR",
                "PA",
                "PR",
                "RI",
                "SC",
                "SD",
                "TN",
                "TX",
                "UT",
                "VT",
                "VA",
                "VI",
                "WA",
                "WV",
                "WI",
                "WY"
            };
        }

        void GetUserInfo()
        {
            try
            {
                if (!string.IsNullOrEmpty(SharedPreferences.LoginUserInfo))
                {
                    var userInfo = JsonConvert.DeserializeObject<DisplayUserModel>(SharedPreferences.LoginUserInfo);
                    if (userInfo != null)
                    {
                        Address1 = userInfo.Address1;
                        Address2 = userInfo.Address2;
                        Address3 = userInfo.Address3;
                        City = userInfo.City;
                        State = userInfo.State;
                        Zip = userInfo.Zip;
                        Email.Value = userInfo.Email;
                        UserName.Value = userInfo.UserName;
                        Cell.Value = userInfo.CellPhone;
                        if (userInfo.Purchases != null)
                        {
                            Cigar = userInfo.Purchases.Cigar;
                            Cigarette = userInfo.Purchases.Cigarette;
                            Mst = userInfo.Purchases.Mst;
                            OTDN = userInfo.Purchases.OTDN;
                            TobaccoMember = userInfo.Purchases.TobaccoMember;
                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}