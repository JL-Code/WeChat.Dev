using System.Collections.Generic;
using WeChat.Domain.AggregatesModel;

namespace WeChat.Application
{
    public interface IWeChatAppService
    {
        WeChatAppConfig GetApp(string appCode);

        List<WeChatAppConfig> ListApps();
    }
}
