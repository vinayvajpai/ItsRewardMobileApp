/*
 * 04/20/2022 ALI - Added Portal Message for invalid username/password
 */
using System.Threading.Tasks;
using itsRewards.Services.Base.ServiceProvider;
using itsRewards.Models;
using itsRewards.Models.Auths;
using itsRewards.Helpers;
using System.Text;
using System;
using System.Collections.Generic;

namespace itsRewards.Services.Auths
{
    public class AccountService : IAccountService
    {
        private readonly IRequestProvider _requestProvider;

        public AccountService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<DisplayUserModel> Login(string username, string password)
        {
            var uri = UriHelper.CombineUri(EndPoints.Login);
            var Result = await _requestProvider.GetAuthAsync<DisplayUserModel>(uri, username, password);
            if (Result != null && string.IsNullOrEmpty(Result.Email) && string.IsNullOrEmpty(Result.CellPhone))
                Result.PortalMessage = "Invalid Username or Password";
           
            return Result;
        }

        public async Task<ApiResponse<string>> Register(RegistrationRequestModel request)
        {
            var uri = UriHelper.CombineUri(EndPoints.Register);
            if(request.IsRegistered)
                uri = UriHelper.CombineUri($"{EndPoints.Register}/true");
            else
                uri = UriHelper.CombineUri($"{EndPoints.Register}/false");
           
            return await _requestProvider.PostAuthAsync<ApiResponse<string>, RegistrationRequestModel>(uri, request,request.Username,request.Password);
        }

        public async Task<ApiResponse<string>> ForgotPassword(ForgotPasswordModel request)
        {
            var uri = UriHelper.CombineUri(EndPoints.ForgotPassword);
            return await _requestProvider.PostAsync<ApiResponse<string>, ForgotPasswordModel>(uri, request);
        }

        public async Task<ApiResponse<DisplayUserModel>> GetUser()
        {
            var uri = UriHelper.CombineUri(EndPoints.GetUser);
            return await _requestProvider.GetAsync<ApiResponse<DisplayUserModel>>(uri);
        }

        public async Task<ApiResponse<string>> ResetPassword(ResetPasswordModel request)
        {
            var uri = UriHelper.CombineUri(EndPoints.ResetPassword);
            return await _requestProvider.PostAsync<ApiResponse<string>, ResetPasswordModel>(uri, request);
        }

        public async Task<ApiResponse<string>> VerifyEmail(VerifyEmailModel request)
        {
            var uri = UriHelper.CombineUri(EndPoints.VerifyEmail);
            return await _requestProvider.PostAsync<ApiResponse<string>, VerifyEmailModel>(uri, request);
        }

        public async Task<ApiResponse<string>> ConfirmPin(string userName,string password)
        {
            var uri = UriHelper.CombineUri(EndPoints.Confirmation);
            return await _requestProvider.PostWithoutData<ApiResponse<string>>(uri,userName,password);
        }

        public async Task<ApiResponse<string>> NewPin(string token)
        {
            var uri = UriHelper.CombineUri(EndPoints.NewPin);
            return await _requestProvider.GetAsync<ApiResponse<string>>(uri, token);
        }
    }
}