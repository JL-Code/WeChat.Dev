using Zap.WeChat.SDK.IServices;
using Zap.WeChat.SDK.MessageAPI;

namespace Zap.WeChat.SDK.Handler
{
    public class MessageHandler : BaseService
    {
        public MessageHandler(ICorpAppService service, string appcode)
            : base(service, appcode)
        {
        }

        public MessageHandler(ICorpAppService service, int agentId)
            : base(service, agentId)
        {
        }

        public MessageResult SendText(string message, string toUser)
        {
            return MessageApi.SendText(AppConfig.CorpId, AppConfig.Secret, AppConfig.AgentId.GetValueOrDefault(), message, toUser);
        }

        public MessageResult SendNews(NewsBody body, string toUser = null, string toParty = null, string toTag = null, int safe = 0)
        {
            return MessageApi.SendNews(AppKey, AppConfig.AgentId.GetValueOrDefault(), body, toUser, toParty, toTag, safe);
        }
    }
}
