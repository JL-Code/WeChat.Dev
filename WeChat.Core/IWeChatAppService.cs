using System.Collections.Generic;
using WeChat.Domain.AggregatesModel;

namespace Zap.WeChat.SDK
{
    public interface IWeChatAppService
    {
        WeChatAppConfig GetApp(string appCode);

        List<WeChatAppConfig> ListApps();
    }
}
