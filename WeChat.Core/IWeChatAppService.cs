using System.Collections.Generic;
using WeChat.Domain.AggregatesModel;

namespace WeChat.Core
{
    public interface IWeChatAppService
    {
        WeChatAppConfig GetApp(string appCode);

        List<WeChatAppConfig> ListApps();
    }
}
