using Newtonsoft.Json;

namespace itsRewards.Models.Altrias
{
    public class TokenResponse
    {
        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }
        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }
        [JsonProperty(PropertyName = "ext_expires_in")]
        public int ExtExpiresIn { get; set; }
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }
    }
}