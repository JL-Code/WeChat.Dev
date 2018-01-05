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
        public long ExpiresIn { get; set; }

        [JsonProperty("refresh_token", NullValueHandling = NullValueHandling.Ignore)]
        public string RefreshToken { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [JsonProperty("userid", NullValueHandling = NullValueHandling.Ignore)]
        public string UserId { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        [JsonProperty(".issued", NullValueHandling = NullValueHandling.Ignore)]
        public string Issued { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        [JsonProperty(".expires", NullValueHandling = NullValueHandling.Ignore)]
        public string Expires { get; set; }
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