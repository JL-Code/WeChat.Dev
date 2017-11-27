using Senparc.Weixin.HttpUtility;
using Zap.WeChat.SDK.Entities;

namespace Zap.WeChat.SDK.CommandAPIs
{
    /// <summary>
    /// 通用基础API
    /// </summary>
    public abstract class CommonApi
    {
        public const string API_URL = "https://qyapi.weixin.qq.com/cgi-bin";

        #region 同步方法

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="corpSecret"></param>
        public static TokenResult GetToken(string corpId, string corpSecret)
        {
            #region 主动调用的频率限制
            /*
当你获取到AccessToken时，你的应用就可以成功调用企业号后台所提供的各种接口以管理或访问企业号后台的资源或给企业号成员发消息。

为了防止企业应用的程序错误而引发企业号服务器负载异常，默认情况下，每个企业号调用接口都有一定的频率限制，当超过此限制时，调用对应接口会收到相应错误码。

以下是当前默认的频率限制，企业号后台可能会根据运营情况调整此阈值：

基础频率
每企业调用单个cgi/api不可超过1000次/分，30000次/小时

每ip调用单个cgi/api不可超过2000次/分，60000次/小时

每ip获取AccessToken不可超过300次/小时

发消息频率
每企业不可超过200次/分钟；不可超过帐号上限数*30人次/天

创建帐号频率
每企业创建帐号数不可超过帐号上限数*3/月
*/
            #endregion

            var url = string.Format(API_URL + "/gettoken?corpid={0}&corpsecret={1}", corpId.AsUrlData(), corpSecret.AsUrlData());
            var result = Get.GetJson<TokenResult>(url);
            return result;
        }

        ///// <summary>
        ///// 获取微信服务器的ip段
        ///// </summary>
        ///// <param name="accessToken"></param>
        ///// <returns></returns>
        //public static GetCallBackIpResult GetCallBackIp(string accessToken)
        //{
        //    var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/getcallbackip?access_token={0}", accessToken.AsUrlData());

        //    return Get.GetJson<GetCallBackIpResult>(url);
        //}

        ///// <summary>
        ///// 获取调用微信JS接口的临时票据
        ///// </summary>
        ///// <param name="corpId"></param>
        ///// <param name="corpSecret"></param>
        ///// <returns></returns>
        //public static JsApiTicketResult GetTicket(string corpId, string corpSecret)
        //{
        //    var accessToken = AccessTokenContainer.TryGetToken(corpId, corpSecret);
        //    //var accessToken = GetToken(corpId, corpSecret).access_token;

        //    var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/get_jsapi_ticket?access_token={0}",
        //                            accessToken.AsUrlData());

        //    JsApiTicketResult result = Get.GetJson<JsApiTicketResult>(url);
        //    return result;
        //}



        ///// <summary>
        ///// userid转换成openid接口
        ///// </summary>
        ///// <param name="accessToken"></param>
        ///// <param name="userId">企业号内的成员id</param>
        ///// <param name="agentId">需要发送红包的应用ID，若只是使用微信支付和企业转账，则无需该参数</param>
        ///// <param name="timeOut"></param>
        ///// <returns></returns>
        //public static ConvertToOpenIdResult ConvertToOpenId(string accessToken, string userId, string agentId = null, int timeOut = Config.TIME_OUT)
        //{
        //    var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/convert_to_openid?access_token={0}",
        //        accessToken.AsUrlData());

        //    var data = new
        //    {
        //        userid = userId,
        //        agentid = agentId
        //    };

        //    return CommonJsonSend.Send<ConvertToOpenIdResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        //}

        ///// <summary>
        ///// openid转换成userid接口
        ///// </summary>
        ///// <param name="accessToken"></param>
        ///// <param name="openId"></param>
        ///// <param name="timeOut"></param>
        ///// <returns></returns>
        //public static ConvertToUserIdResult ConvertToUserId(string accessToken, string openId, int timeOut = Config.TIME_OUT)
        //{
        //    var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/convert_to_userid?access_token={0}",
        //        accessToken.AsUrlData());

        //    var data = new
        //    {
        //        openid = openId
        //    };

        //    return CommonJsonSend.Send<ConvertToUserIdResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        //}
        #endregion
    }

    
}
