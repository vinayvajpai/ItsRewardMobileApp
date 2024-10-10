/*
 * 04/20/2022 ALI - Fixed to allow bi-directional communication 
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using itsRewards.Extensions;
using System.Linq;
using System.IO;
using itsRewards.Services.Base.NavigationServices;
using System.Text;
using itsRewards.Models.Auths;
using itsRewards.Models;

namespace itsRewards.Services.Base.ServiceProvider
{
    public class RequestProvider : IRequestProvider
    {
        private readonly JsonSerializerSettings _serializerSettings;
        protected INavigationService _navigationService => itsRewards.Views.AppShell.Resolve<INavigationService>();

        public RequestProvider()
        {
            _serializerSettings = new JsonSerializerSettings
            {
               ContractResolver = new CamelCasePropertyNamesContractResolver(),
               //DateTimeZoneHandling = DateTimeZoneHandling.Local,
               NullValueHandling = NullValueHandling.Ignore
            };
            _serializerSettings.Converters.Add(new StringEnumConverter());
        }

             /// <summary>
             /// Logs into the portal for getting user name/password
             /// </summary>
             /// <typeparam name="TResult"></typeparam>
             /// <param name="uri"></param>
             /// <param name="username"></param>
             /// <param name="password"></param>
             /// <returns></returns>

        public async Task<string> GetHtmlAsync(string uri)
        {
            Debug.WriteLine("API URL: " + uri);

            HttpClient httpClient = CreateHttpClient();
            var url = new Uri(uri, UriKind.Absolute);
            HttpResponseMessage response = await httpClient.GetAsync(url);
             HandleResponse(response);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<TResult> GetAsync<TResult>(string uri, string token = "", List<KeyValuePair<string, string>> headers = null)
        {
            Debug.WriteLine("API URL: "+ uri);

            HttpClient httpClient = CreateHttpClient(token,headers);

            var url = new Uri(uri,UriKind.Absolute);
            HttpResponseMessage response = await httpClient.GetAsync(uri);

            HandleResponse(response);
            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() =>
                JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));
            Console.WriteLine(result);
            return result;
        }

        public async Task<TResult> GetAsync<TResult>(string uri, string token = "")
        {
            Debug.WriteLine("API URL: " + uri);

           

            HttpClient httpClient = CreateHttpClient(token);

            HttpResponseMessage response = await httpClient.GetAsync(uri);

             HandleResponse(response);
            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() =>
                JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));
            Console.WriteLine(result);
            return result;
        }

        /// <summary>
        /// Logs into the portal for getting user name/password
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="uri"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<TResult> GetAuthAsync<TResult>(string uri, string username, string password)
            where TResult : new()
        {
            Debug.WriteLine("API URL: " + uri);
            HttpClient httpClient = CreateHttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"{username}:{password}")));

            HttpResponseMessage response = await httpClient.GetAsync(uri);
            HandleResponse(response);
            string serialized = await response.Content.ReadAsStringAsync();
            
            TResult result = await Task.Run(() =>
                JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));
            if (result == null && response.StatusCode == HttpStatusCode.Unauthorized)
            {
                TResult result1 = new TResult();
                return result1;
            }
            Console.WriteLine(result);
            return result;
        }

        public async Task<HttpResponseMessage> GetHttpResponseMessage(string uri)
        {
            HttpClient httpClient = CreateHttpClient(null);
            return await httpClient.GetAsync(uri);
        }

        public async Task<TResult> PostAsync<TResult, TRequest>(string uri, TRequest data, string token = "", List<KeyValuePair<string, string>> headers = null)
        {
            Debug.WriteLine("API URL: " + uri);

            HttpClient httpClient = CreateHttpClient(token, headers);

            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PostAsync(uri, content);

             HandleResponse(response);
            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() =>
                JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));
            Console.WriteLine(result);
            return result;
        }

        public async Task<TResult> PostAuthAsync<TResult, TRequest>(string uri, TRequest data, string username,string password)
        {
            Debug.WriteLine("API URL: " + uri);

            HttpClient httpClient = CreateHttpClient();
            var authToken = string.Empty;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                authToken = SharedPreferences.AuthToken;
            }
            else
            {
                authToken = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"{username}:{password}"));
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PostAsync(uri, content);

             HandleResponse(response);
            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() =>
                JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));
            if(response.StatusCode == HttpStatusCode.OK)
            {
                SharedPreferences.AuthToken = authToken;
            }
            Console.WriteLine(result);
            return result;
        }

        public async Task<TResult> PostWithoutData<TResult>(string url, string username,string password)
        {
            Debug.WriteLine("API URL: " + url);

            HttpClient httpClient = CreateHttpClient();
            var authToken = string.Empty;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                authToken = SharedPreferences.AuthToken;
            }
            else
            {
                authToken = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"{username}:{password}"));
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

            
            HttpResponseMessage response = await httpClient.PostAsync(url, null);

            HandleResponse(response);
            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() =>
                JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));
            if (response.StatusCode == HttpStatusCode.OK)
            {
                SharedPreferences.AuthToken = authToken;
            }
            Console.WriteLine(result);
            return result;
        }
        public async Task<TResult> PostUrlEncodedAsync<TResult>(string uri, List<KeyValuePair<string, string>> data, string token = "", List<KeyValuePair<string, string>> headers = null)
        {
            Debug.WriteLine("API URL: " + uri);
            HttpClient httpClient = CreateHttpClient(token,headers);

            var content = new FormUrlEncodedContent(data);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            HttpResponseMessage response = await httpClient.PostAsync(uri, content);

             HandleResponse(response);
            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() =>
                JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));
            Console.WriteLine(result);
            return result;
        }

        public async Task<TResult> PutAsync<TResult>(string uri, TResult data, string token = "", List<KeyValuePair<string, string>> headers = null)
        {
            Debug.WriteLine("API URL: " + uri);
            HttpClient httpClient = CreateHttpClient(token,headers);

            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PutAsync(uri, content);
          
             HandleResponse(response);
            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() =>
                JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));
            Console.WriteLine(result);
            return result;
        }

        public async Task<TResult> PutAsync<TResult>(string uri, string username, string password)
        {
            Debug.WriteLine("API URL: " + uri);
            var authToken = string.Empty;
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                authToken = SharedPreferences.AuthToken;
            }
            else
            {
                authToken = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"{username}:{password}"));
            }

            HttpClient httpClient = CreateHttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);
           
            HttpResponseMessage response = await httpClient.PutAsync(uri, null);

            HandleResponse(response);
            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() =>
                JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));
            Console.WriteLine(result);
            return result;
        }

        public async Task DeleteAsync(string uri, string token = "")
        {
            Debug.WriteLine("API URL: " + uri);
            HttpClient httpClient = CreateHttpClient(token);
            await httpClient.DeleteAsync(uri);
        }


        public async Task<TResult> UploadFile<TResult>(string uri, Stream data, string filename, string token = "", List<KeyValuePair<string, string>> headers = null)
        {
            
            Debug.WriteLine("API URL: " + uri);

            HttpClient httpClient = CreateHttpClient(token, headers);

            HttpContent fileStreamContent = new StreamContent(data);
            var name = filename.Split('/').Last();

            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            httpClient.DefaultRequestHeaders.Add("binaryStringRequestBody", "true");
          
            fileStreamContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data") { Name = "file", FileName = name };
            fileStreamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(fileStreamContent);
                HttpResponseMessage response = await httpClient.PostAsync(uri, formData);

                 HandleResponse(response);
                string serialized = await response.Content.ReadAsStringAsync();

                TResult result = await Task.Run(() =>
                    JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));
                Console.WriteLine(result);
                return result;
            }
        }

        private HttpClient CreateHttpClient(string token = "", List<KeyValuePair<string, string>> headers = null)
        {
            if (string.IsNullOrEmpty(token))
            {
                token = SharedPreferences.AuthToken;
            }
            #region For SSL connection error use the following code
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            #endregion
            var httpClient = new HttpClient(clientHandler);
            
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            if (headers != null && headers.Count > 0)
            {
                foreach (var header in headers)
                {
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            return httpClient;
        }

        private void AddBasicAuthenticationHeader(HttpClient httpClient, string clientId, string clientSecret)
        {
            if (httpClient == null)
                return;

            if (string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(clientSecret))
                return;

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(clientId, clientSecret);
        }

        private /*async Task*/ void HandleResponse(HttpResponseMessage response)
        {
            //if (response.IsSuccessStatusCode)
            //{
            //    if (response.RequestMessage.RequestUri.ToString().Contains(EndPoints.Login))
            //    {
            //        var mtoken = response.Headers.GetValues("Token").ToList();
            //        SharedPreferences.AuthToken = mtoken[0];
            //    }
            //}

#if notused
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                SharedPreferences.AuthToken = string.Empty;
                await _navigationService.GoToAsync("//login");
            }
#endif
        }
    }
}
