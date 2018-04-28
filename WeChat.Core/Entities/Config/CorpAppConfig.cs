using System;
using System.Xml.Serialization;

namespace Zap.WeChat.SDK.Entities
{
    public class CorpAppConfig : CorpConfig
    {
        /// <summary>
        /// 应用秘钥
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// 应用ID
        /// </summary>
        [XmlIgnore]
        public int AgentId { get; set; }

        /// <summary>
        /// 用于解决数字类型空值时XML反序列化报错
        /// </summary>
        [XmlElement("AgentId")]
        public string AgentIdStr
        {
            get { return AgentId.ToString(); }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    AgentId = 0;
                }
                else
                {
                    try
                    {
                        AgentId = Convert.ToInt32(value);
                    }
                    catch (Exception)
                    {
                        AgentId = 0;
                    }
                }
            }
        }

        /// <summary>
        /// 应用编码
        /// </summary>
        public string AppCode { get; set; }

        /// <summary>
        /// 消息令牌
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 消息加密串
        /// </summary>
        public string EncodingAESKey { get; set; }
    }
}
