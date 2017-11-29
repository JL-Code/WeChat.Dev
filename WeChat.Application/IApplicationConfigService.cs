using System.Collections.Generic;
using WeChat.Domain.AggregatesModel;

namespace WeChat.Application
{
    public interface IApplicationConfigService
    {

        List<ApplicationConfig> ListApplicationConfig();
    }
}
