using Newtonsoft.Json;

namespace Zap.WeChat.SDK.MessageAPI
{
    public class MessageResult : WeChatResponse
    {
        /// <summary>
        /// 无效的用户
        /// </summary>
        [JsonProperty("invaliduser", NullValueHandling = NullValueHandling.Ignore)]
        public string InvalidUser { get; set; }
        /// <summary>
        /// 无效的部门
        /// </summary>
        [JsonProperty("invalidparty", NullValueHandling = NullValueHandling.Ignore)]
        public string InvalidParty { get; set; }
        /// <summary>
        /// 无效的标签
        /// </summary>
        [JsonProperty("invalidtag", NullValueHandling = NullValueHandling.Ignore)]
        public string InvalidTag { get; set; }
    }
}
