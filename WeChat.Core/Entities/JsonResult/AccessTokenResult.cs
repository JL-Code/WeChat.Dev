using Newtonsoft.Json;

namespace Zap.WeChat.SDK.Entities
{
    public class TokenResult : WeChatResponse
    {
        /// <summary>
        /// 访问令牌
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
