using Newtonsoft.Json;

namespace Zap.WeChat.SDK.MessageAPI
{
    public class TextBody
    {
        /// <summary>
        /// 消息内容
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; set; }
    }
}
