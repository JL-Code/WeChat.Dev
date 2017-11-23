using WeChat.Core.Cache;
using WeChat.Core.MessageAPI;

namespace WeChat.Core.Implementation
{
    public class MessageService : IMessageService
    {
        private readonly string corpId = LocalCacheManager.Get<string>(Constants.CORP_ID);
        IWeChatAppService _appService;


        public MessageService(IWeChatAppService appService)
        {
            _appService = appService;
        }


        public void SendText(string appcode, string message)
        {
            var app = _appService.GetApp(appcode);
            var accessToken = AccessTokenManager.GetToken(corpId, app.SecretValue);
            MessageApi.SendText(accessToken, app.WeChatAppID, message);
        }
    }
}
