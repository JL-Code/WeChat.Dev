namespace WeChat.Application
{
    public interface IMessageService
    {
        /// <summary>
        /// 发送文本消息
        /// </summary>
        /// <param name="appcode">应用编码</param>
        /// <param name="message">消息</param>
        /// <param name="toUser">成员ID列表（消息接收者，多个接收者用‘|’分隔，最多支持1000个）。特殊情况：指定为@all，则向该企业应用的全部成员发送</param>
        /// <param name="toParty">部门ID列表，多个接收者用‘|’分隔，最多支持100个。当touser为@all时忽略本参数</param>
        void SendText(string appcode, string message, string toUser = null, string toParty = null);
    }
}
