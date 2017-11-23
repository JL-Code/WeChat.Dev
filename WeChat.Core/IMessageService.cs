namespace WeChat.Core
{
    public interface IMessageService
    {
        /// <summary>
        /// 发送文本消息
        /// </summary>
        /// <param name="appcode">应用编码</param>
        /// <param name="message">消息</param>
        void SendText(string appcode, string message);
    }
}
