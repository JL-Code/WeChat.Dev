using Newtonsoft.Json;

namespace Zap.WeChat.SDK.AdvancedAPIs.OAuth2
{
    public class GetUserInfoResult : WeChatResponse
    {
        /// <summary>
        /// 员工UserID
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 非企业成员的OpenId
        /// （此属性在Work最新文档中没有）
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 手机设备号(由微信在安装时随机生成) 
        /// （此属性在Work最新文档中没有）
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// 成员票据，最大为512字节。
        /// scope为snsapi_userinfo或snsapi_privateinfo，且用户在应用可见范围之内时返回此参数。
        /// 后续利用该参数可以获取用户信息或敏感信息。
        /// </summary>
        [JsonProperty("user_ticket")]
        public string UserTicket { get; set; }

        /// <summary>
        /// user_token的有效时间（秒），随user_ticket一起返回
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
