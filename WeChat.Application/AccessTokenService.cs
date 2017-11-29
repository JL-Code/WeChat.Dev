using Zap.WeChat.SDK;
using Zap.WeChat.SDK.Cache;

namespace WeChat.Application
{
    public abstract class AccessTokenService
    {
        protected IWeChatAppService _appService;

        public AccessTokenService(IWeChatAppService appService)
        {
            _appService = appService;
        }

        public string CorpID
        {
            get
            {
                return LocalCacheManager.Get<string>(Constants.CORP_ID);
            }
        }

        public virtual AppResult GetToken(string appcode)
        {

            var app = _appService.GetApp(appcode);
            var accessToken = AccessTokenManager.GetToken(CorpID, app.SecretValue);
            return new AppResult
            {
                AccessToken = accessToken,
                AgentId = app.WeChatAppID
            };
        }

        public virtual AppResult TryGetToken(string appcode)
        {

            var app = _appService.GetApp(appcode);
            var accessToken = AccessTokenManager.TryGetToken(CorpID, app.SecretValue);
            return new AppResult
            {
                AccessToken = accessToken,
                AgentId = app.WeChatAppID
            };
        }
    }
}
