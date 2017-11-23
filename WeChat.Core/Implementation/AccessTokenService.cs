using WeChat.Core.Cache;

namespace WeChat.Core.Implementation
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

        public virtual App GetToken(string appcode)
        {

            var app = _appService.GetApp(appcode);
            var accessToken = AccessTokenManager.GetToken(CorpID, app.SecretValue);
            return new App
            {
                AccessToken = accessToken,
                AgentId = app.WeChatAppID
            };
        }


    }
}
