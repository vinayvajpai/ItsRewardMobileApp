using System.Threading.Tasks;
using itsRewards.Models;
using itsRewards.Models.Auths;

namespace itsRewards.Services.Auths
{
    public interface IAccountService
    {
        Task<ApiResponse<string>> Register(RegistrationRequestModel request);
        Task<DisplayUserModel> Login(string username, string password);
        Task<ApiResponse<string>> ResetPassword(ResetPasswordModel request);
        Task<ApiResponse<string>> ForgotPassword(ForgotPasswordModel request);
        Task<ApiResponse<string>> VerifyEmail(VerifyEmailModel request);
        Task<ApiResponse<DisplayUserModel>> GetUser();
        Task<ApiResponse<string>> ConfirmPin(string userName, string password);
        Task<ApiResponse<string>> NewPin(string token);
    }
}
