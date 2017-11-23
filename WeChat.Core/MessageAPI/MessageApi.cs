using Senparc.Weixin.Work.AdvancedAPIs;
using Senparc.Weixin.Work.Containers;

namespace WeChat.Core.MessageAPI
{
    public class MessageApi
    {

        /// <summary>
        /// 发送文本消息
        /// </summary>
        /// <param name="accessToken">访问令牌</param>
        /// <param name="agentId">应用ID</param>
        /// <param name="content">文本内容</param>
        /// <param name="toUser"></param>
        /// <param name="toParty"></param>
        /// <param name="toTag"></param>
        /// <param name="safe"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static MessageResult SendText(string accessToken, int agentId, string content, string toUser = null, string toParty = null, string toTag = null, int safe = 0, int timeOut = 10000)
        {
            var result = MassApi.SendText(accessToken, agentId.ToString(), content, toUser, toParty, toTag, safe, timeOut);
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
        public static MessageResult SendText(string corpId, string corpSecret, int agentId, string content, string toUser = null, string toParty = null, string toTag = null, int safe = 0, int timeOut = 10000)
        {
            var appKey = AccessTokenContainer.BuildingKey(content, corpSecret);
            return SendText(appKey, agentId, content, toUser, toParty, toTag, safe, timeOut);
        }
    }
}
