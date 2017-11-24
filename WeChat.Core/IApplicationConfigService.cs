using System.Collections.Generic;
using WeChat.Domain.AggregatesModel;

namespace Zap.WeChat.SDK
{
    public interface IApplicationConfigService
    {

        List<ApplicationConfig> ListApplicationConfig();
    }
}
