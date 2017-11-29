using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zap.WeChat.SDK.Entities;
using Zap.WeChat.SDK.Helpers;
using Zap.WeChat.SDK.IServices;

namespace Zap.WeChat.SDK.Handler
{
    /// <summary>
    /// JSSDK 封装类
    /// </summary>
    public class JSSDKHandler : BaseService
    {
        public JSSDKHandler(ICorpAppService service, string appcode)
            : base(service, appcode)
        {
        }

        public JSSDKHandler(ICorpAppService service, int agentId)
            : base(service, agentId)
        {
        }

        /// <summary>
        /// 获取jsapi权限签名
        /// </summary>
        /// <param name="currentURL">当前URL地址</param>
        /// <returns></returns>
        public JsApiTicketResult GetSignature(string url)
        {
            var jsapi_ticket = JsApiTicketManager.TryGetTicket(Config.CorpId, Config.CorpSecret);
            var timestamp = JSSDKHelper.GetTimestamp();
            var noncestr = JSSDKHelper.GetNoncestr();
            var signature = JSSDKHelper.GetSignature(jsapi_ticket, noncestr, timestamp, url);

            return new JsApiTicketResult
            {
                AppId = Config.CorpId, // 必填，企业号的唯一标识，此处填写企业号corpid
                Timestamp = timestamp, // 必填，生成签名的时间戳
                Noncestr = noncestr, // 必填，生成签名的随机串
                Signature = signature,// 必填，签名，见附录1
            };
        }
    }
}
