using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace itsRewards.Services.Base.ServiceProvider
{
    public interface IRequestProvider
    {
        Task<TResult> GetAsync<TResult>(string uri, string token = "", List<KeyValuePair<string, string>> headers = null);
        Task<string> GetHtmlAsync(string uri);
        Task<TResult> GetAuthAsync<TResult>(string uri, string username, string password)where TResult: new();
        Task<HttpResponseMessage> GetHttpResponseMessage(string uri);
        Task<TResult> PostAsync<TResult, TRequest>(string uri, TRequest data, string token = "", List<KeyValuePair<string, string>> headers = null);
        Task<TResult> PostAuthAsync<TResult, TRequest>(string uri, TRequest data, string username, string password);
        Task<TResult> PostUrlEncodedAsync<TResult>(string uri, List<KeyValuePair<string, string>> data, string token = "", List<KeyValuePair<string, string>> headers = null);
        Task<TResult> PostWithoutData<TResult>(string url, string username, string password);

        Task<TResult> PutAsync<TResult>(string uri, TResult data, string token = "", List<KeyValuePair<string, string>> headers = null);
        Task<TResult> PutAsync<TResult>(string uri, string username, string password);
        Task DeleteAsync(string uri, string token = "");
        Task<TResult> UploadFile<TResult>(string uri, Stream data, string filename, string token = "", List<KeyValuePair<string, string>> headers = null);
    }
}