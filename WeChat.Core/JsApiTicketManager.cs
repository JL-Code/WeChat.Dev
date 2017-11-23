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
    }
}
