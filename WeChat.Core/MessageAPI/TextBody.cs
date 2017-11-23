using Newtonsoft.Json;

namespace WeChat.Core.MessageAPI
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
