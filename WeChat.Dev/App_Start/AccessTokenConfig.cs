using System.Collections.Generic;
using WeChat.Core;
using WeChat.Core.Cache;
using WeChat.Domain.AggregatesModel;

namespace WeChat.Dev
{
    /// <summary>
    /// 微信令牌信息配置
    /// </summary>
    public class AccessTokenConfig
    {
        //企业ID
        const string corpId = "wx2e8cc6975a5fa1ce";
        public static void Register()
        {
            IWeChatAppService appService = AutofacManager.Resolve<IWeChatAppService>();
            LocalCacheManager.Add(Constants.CORP_ID, corpId);
            
            List<WeChatAppConfig> apps = appService.ListApps();
            apps.ForEach(app =>
            {
                WeChatManager.RegisterWorkApp(corpId, app.SecretValue, app.AppName);
            });
        }
    }
}