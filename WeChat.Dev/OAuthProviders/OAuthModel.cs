using Newtonsoft.Json;

namespace WeChat.Dev.OAuthProviders
{
    public class OAuthModel : OAuthExpception
    {
        [JsonProperty("access_token", NullValueHandling = NullValueHandling.Ignore)]
        public string AccesssToken { get; set; }

        [JsonProperty("token_type", NullValueHandling = NullValueHandling.Ignore)]
        public string TokenType { get; set; }

        [JsonProperty("expires_in", NullValueHandling = NullValueHandling.Ignore)]
        public int ExpiresIn { get; set; }

        [JsonProperty("refresh_token", NullValueHandling = NullValueHandling.Ignore)]
        public string RefreshToken { get; set; }
    }

    public abstract class OAuthExpception
    {
        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public string Error { get; set; }
        [JsonProperty("error_description", NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorDescription { get; set; }

    }
}