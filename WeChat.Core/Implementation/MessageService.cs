using Zap.WeChat.SDK.MessageAPI;

namespace Zap.WeChat.SDK.Implementation
{
    public class MessageService : AccessTokenService, IMessageService
    {
        public MessageService(IWeChatAppService appService)
            : base(appService)
        {
        }

        public void SendText(string appcode, string message, string toUser = null, string toParty = null)
        {
            var result = GetToken(appcode);
            MessageApi.SendText(result.AccessToken, result.AgentId, message, toUser, toParty);
        }
    }
}
