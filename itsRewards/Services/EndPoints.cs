namespace itsRewards.Services
{
    public class EndPoints
    {
        public const string AltriaBase = "https://api.insightsc3m.com/altria/oauth2/v2.0/";
        public const string AltriaClientId = "91b467a7-656b-4b04-9a73-1c0232016354";
        public const string AltriaClientSecret = "g0n1goUah4soiMkrA3obH39aEVNmf+B2eV3Mrqa9RX8=";
        public const string AltriaSubscriptionKey = "c94cc924617d438f895bb924a217a9e0";
        public const string AltriaTokenSubscriptionKey = "ebc86273b3db461b9b06de407c9d230d";

        public const string AltriaTestEnvironmentScope = "https://api.dev.insightsc3m.com/.default";
        public const string AltriaProductionEnvironmentScope = "https://api.insightsc3m.com/.default";

        public const string AltriaToken = AltriaBase + "token";
        public const string AltriaGetCoupon = "https://api.insightsc3m.com/rdcapp/index.html?access-token={0}&subscription-key={1}&brand={2}&lat={3}&lon={4}&color={5}&loyalty_id={6}";

        public const string Base = "http://smokinrebate.us:8082/API/V1/Loyalty/";
        
        #region Account
        public const string Login = Base + "Signin/0";
        public const string Register = Base + "Signup";
        public const string GetUser = Base + "api/account/getuser";
        public const string ForgotPassword = Base + "api/account/forgotpassword";
        public const string ResetPassword = Base + "api/account/resetpassword";
        public const string VerifyEmail = Base + "api/account/verifyemail";
        public const string Confirmation = Base + "Confirmed";
        public const string NewPin = Base + "NewPin";
        #endregion
    }
}