using itsRewards.Models.Auths;
using Xamarin.Essentials;

namespace itsRewards.Extensions
{
    public class SharedPreferences
    {
        public static string AuthToken
        {
            get { return Preferences.Get("AuthToken", string.Empty); }
            set { Preferences.Set("AuthToken", value); }
        }

        public static string AltriaAccessToken
        {
            get { return Preferences.Get("AltriaAccessToken", string.Empty); }
            set { Preferences.Set("AltriaAccessToken", value); }
        }

        public static string UserId
        {
            get { return Preferences.Get("UserId", string.Empty); }
            set { Preferences.Set("UserId", value); }
        }

        public static bool IsRememberMe
        {
            get { return Preferences.Get("IsRememberMe", false); }
            set { Preferences.Set("IsRememberMe", value); }
        }

        public static string Email
        {
            get { return Preferences.Get("Email", string.Empty); }
            set { Preferences.Set("Email", value); }
        }

        public static string Password
        {
            get { return Preferences.Get("Password", string.Empty); }
            set { Preferences.Set("Password", value); }
        }

        public static string AccountNumber
        {
            get { return Preferences.Get("AccountNumber", string.Empty); }
            set { Preferences.Set("AccountNumber", value); }
        }

        public static string LoginUserInfo
        {
            get { return Preferences.Get("LoginUserInfo", string.Empty); }
            set { Preferences.Set("LoginUserInfo", value); }
        }
    }
}