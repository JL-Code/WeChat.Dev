using Newtonsoft.Json;

namespace Zap.WeChat.SDK.Entities
{
    public class JsApiTicketResult
    {
        /// <summary>
        /// 必填，企业号的唯一标识，此处填写企业号corpid
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        ///  必填，生成签名的时间戳
        /// </summary>
        public long Timestamp { get; set; }

        /// <summary>
        ///  必填，生成签名的随机串
        /// </summary>
        public string Noncestr { get; set; }

        /// <summary>
        ///  必填 签名
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// jsapi_ticket
        /// </summary>
        [JsonIgnore]
        public string JsapiTicket { get; set; }
    }
}
