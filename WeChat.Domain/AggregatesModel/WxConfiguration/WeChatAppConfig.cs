using System;
using WeChat.Domain.SeedWork;

namespace WeChat.Domain.AggregatesModel
{
    public class WeChatAppConfig : Entity, IAggregateRoot
    {
        /// <summary>
        /// 应用GUID
        /// </summary>
        public System.Guid AppGUID { get; set; }
        /// <summary>
        /// 应用名称
        /// </summary>
        public string AppName { get; set; }
        /// <summary>
        /// 应用code
        /// </summary>
        public string AppCode { get; set; }
        /// <summary>
        /// 应用ID（int）
        /// </summary>
        public int WeChatAppID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string EncodingAESKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UrlParam { get; set; }
        /// <summary>
        /// 应用秘钥
        /// </summary>
        public string SecretValue { get; set; }
    }
}
