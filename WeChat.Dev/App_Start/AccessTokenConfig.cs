using System.Collections.Generic;
using System.Linq;
using Zap.WeChat.SDK;
using WeChat.Domain.AggregatesModel;
using WeChat.Application;
using Zap.WeChat.SDK.Cache;

namespace WeChat.Dev
{
    /// <summary>
    /// 微信令牌信息配置
    /// </summary>
    public class AccessTokenConfig
    {
        public static void Register()
        {
            IWeChatAppService appService = AutofacManager.Resolve<IWeChatAppService>();
            IApplicationConfigService configService = AutofacManager.Resolve<IApplicationConfigService>();
            var config = configService.ListApplicationConfig()
                .FirstOrDefault(m => m.ConfigType.ToLower() == Constants.WECHAT.ToLower() && m.ConfigKey.ToLower() == Constants.CORP_ID.ToLower());
            LocalCacheManager.Add(Constants.CORP_ID, config.ConfigValue);

            List<WeChatAppConfig> apps = appService.ListApps();
            apps.ForEach(app =>
            {
                WeChatManager.RegisterWorkApp(config.ConfigValue, app.SecretValue, app.AppName);
            });
        }
    }
}