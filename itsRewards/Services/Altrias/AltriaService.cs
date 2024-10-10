using System.Threading.Tasks;
using itsRewards.Services.Base.ServiceProvider;
using itsRewards.Helpers;
using System.Collections.Generic;
using itsRewards.Models.Altrias;
using itsRewards.Models;
using itsRewards.Extensions;
using System.Web;
using System;

namespace itsRewards.Services.Altrias
{
    public class AltriaService : IAltriaService
    {
        private readonly IRequestProvider _requestProvider;

        public AltriaService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<TokenResponse> AltriaToken()
        {
            var uri = UriHelper.CombineUri(EndPoints.AltriaToken);
            List<KeyValuePair<string, string>> headers = new List<KeyValuePair<string, string>>() {
                
                new KeyValuePair<string, string>("Ocp-Apim-Subscription-Key", "ebc86273b3db461b9b06de407c9d230d") //ebc86273b3db461b9b06de407c9d230d EndPoints.AltriaTokenSubscriptionKey),
            };
            
            List<KeyValuePair<string, string>> request = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("client_id", EndPoints.AltriaClientId),
                new KeyValuePair<string, string>("client_secret", EndPoints.AltriaClientSecret),
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
           };

     //      request.Add(new KeyValuePair<string, string>("scope", EndPoints.AltriaTestEnvironmentScope));

            request.Add(new KeyValuePair<string, string>("scope", EndPoints.AltriaProductionEnvironmentScope));

            return await _requestProvider.PostUrlEncodedAsync<TokenResponse>(uri, request, null, headers);
        }

        public async Task<string> GetCouponHtml(string brand)
        {
            string accesstoken = SharedPreferences.AltriaAccessToken;
            string subscriptionkey = EndPoints.AltriaSubscriptionKey;

            var uri = @"https://api.insightsc3m.com/rdcapp/index.html?access-token="+
                accesstoken
                + "&subscription-key="+ subscriptionkey
                + "&brand=" + brand; 
            return await _requestProvider.GetHtmlAsync(uri);
        }
    }
}