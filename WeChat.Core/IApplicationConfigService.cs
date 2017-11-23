using System.Collections.Generic;
using WeChat.Domain.AggregatesModel;

namespace WeChat.Core
{
    public interface IApplicationConfigService
    {

        List<ApplicationConfig> ListApplicationConfig();
    }
}
