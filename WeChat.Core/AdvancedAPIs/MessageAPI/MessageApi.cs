using Senparc.Weixin;
using Senparc.Weixin.Work.AdvancedAPIs;
using Senparc.Weixin.Work.Containers;
using System;
using Zap.WeChat.SDK.Cache;
using Zap.WeChat.SDK.Entities;

namespace Zap.WeChat.SDK.MessageAPI
{
    public class MessageApi
    {

        /// <summary>
        /// 发送文本消息
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口凭证（AccessToken）或AppKey（根据AccessTokenManager.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="agentId">应用ID</param>
        /// <param name="content">文本内容</param>
        /// <param name="toUser"></param>
        /// <param name="toParty"></param>
        /// <param name="toTag"></param>
        /// <param name="safe"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static MessageResult SendText(string accessTokenOrAppKey, int agentId, string content, string toUser = null, string toParty = null, string toTag = null, int safe = 0, int timeOut = Config.TIME_OUT)
        {
            var result = MassApi.SendText(accessTokenOrAppKey, agentId.ToString(), content, toUser, toParty, toTag, safe, timeOut);
            return result.ToMsgResult();
        }

        /// <summary>
        /// 发送文本消息
        /// </summary>
        /// <param name="corpId">企业ID</param>
        /// <param name="corpSecret">企业应用秘钥</param>
        /// <param name="agentId">企业应用ID</param>
        /// <param name="content">文本内容</param>
        /// <param name="toUser"></param>
        /// <param name="toParty"></param>
        /// <param name="toTag"></param>
        /// <param name="safe"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static MessageResult SendText(string corpId, string corpSecret, int agentId, string content, string toUser = null, string toParty = null, string toTag = null, int safe = 0, int timeOut = Config.TIME_OUT)
        {
            var appKey = AccessTokenContainer.BuildingKey(corpId, corpSecret);
            return SendText(appKey, agentId, content, toUser, toParty, toTag, safe, timeOut);
        }

        /// <summary>
        /// 发送图文消息
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenManager.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="toUser">UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param>
        /// <param name="toParty">PartyID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="toTag">TagID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="agentId">企业应用的id，可在应用的设置页面查看</param>
        /// <param name="articles">图文信息内容，包括title（标题）、description（描述）、url（点击后跳转的链接。企业可根据url里面带的code参数校验员工的真实身份）和picurl（图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80。如不填，在客户端不显示图片）</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static MessageResult SendNews(string accessTokenOrAppKey, int agentId, NewsBody body,
                string toUser = null, string toParty = null, string toTag = null, int safe = 0,
                int timeOut = Config.TIME_OUT)
        {
            var result = MassApi.SendNews(accessTokenOrAppKey, agentId.ToString(), body.ToNews(), toUser, toParty, toTag, safe, timeOut);
            return result.ToMsgResult();
        }

        ///// <summary>
        ///// 通过应用编码发送消息 （必须先注册appcode相关的配置信息 否则会报应用未注册的异常）
        ///// </summary>
        ///// <param name="appcode"></param>
        ///// <param name="agentId"></param>
        ///// <param name="body"></param>
        ///// <param name="toUser"></param>
        ///// <param name="toParty"></param>
        ///// <param name="toTag"></param>
        ///// <param name="safe"></param>
        ///// <param name="timeOut"></param>
        ///// <returns></returns>
        //public static MessageResult SendNewsByCode(string appcode, int agentId, NewsBody body,
        //        string toUser = null, string toParty = null, string toTag = null, int safe = 0,
        //        int timeOut = Config.TIME_OUT)
        //{
        //    string accessToken = GetToken(appcode);
        //    return SendNews(accessToken, agentId, body, toUser, toParty, toTag, safe, timeOut);
        //}

        //private static string GetToken(string appcode)
        //{
        //    var app = LocalCacheManager.Get<CorpAppConfig>(appcode);
        //    if (app == null)
        //        throw new NullReferenceException($"应用编码为{appcode}的应用未注册");
        //    var accessToken = AccessTokenContainer.TryGetToken(app.CorpId, app.CorpSecret);
        //    return accessToken;
        //}
    }
}
