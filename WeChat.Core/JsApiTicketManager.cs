using Senparc.Weixin.Work.Containers;

namespace WeChat.Core
{
    public class JsApiTicketManager
    {
        /// <summary>
        /// 获取可用Ticket
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static string GetTicket(string appId, string appSecret, bool getNewTicket = false)
        {
            return JsApiTicketContainer.GetTicket(appId, appSecret, getNewTicket);
        }

        /// <summary>
        /// 使用完整的应用凭证获取Ticket，如果不存在将自动注册
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="getNewTicket"></param>
        /// <returns></returns>
        public static string TryGetTicket(string appId, string appSecret, bool getNewTicket = false)
        {
            return JsApiTicketContainer.TryGetTicket(appId, appSecret, getNewTicket);
        }
    }
}
