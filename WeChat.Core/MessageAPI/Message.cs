using Newtonsoft.Json;
using System.Collections.Generic;

namespace Zap.WeChat.SDK.MessageAPI
{
    public class Message<T> where T : class
    {
        #region Properties

        /// <summary>
        /// 用于存放 动态序列化属性名的字典
        /// </summary>
        public virtual Dictionary<string, string> DictProp { get; }

        /// <summary>
        /// 成员ID列表（消息接收者，多个接收者用‘|’分隔，最多支持1000个）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送  
        /// </summary>
        [JsonProperty("touser")]
        public string ToUser { get; set; }

        /// <summary>
        /// 部门ID列表，多个接收者用‘|’分隔，最多支持100个。当touser为@all时忽略本参数 
        /// </summary>
        [JsonProperty("toparty")]
        public string ToParty { get; set; }

        /// <summary>
        /// 标签ID列表，多个接收者用‘|’分隔。当touser为@all时忽略本参数
        /// </summary>
        [JsonProperty("totag")]
        public string ToTag { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        [JsonProperty("msgtype")]
        public virtual string MsgType { get; }

        /// <summary>
        /// 企业应用的id，整型。可在应用的设置页面查看 
        /// </summary>
        [JsonProperty("agentid")]
        public string AgentId { get; set; }

        /// <summary>
        /// 消息主体
        /// </summary>
        public T MessageBody { get; set; }

        /// <summary>
        /// 表示是否是保密消息，0表示否，1表示是，默认0 
        /// </summary>
        [JsonProperty("safe")]
        public int Safe { get; set; }

        #endregion
    }
}
