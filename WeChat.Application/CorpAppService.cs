using System.Linq;
using WeChat.Domain.AggregatesModel;
using Zap.WeChat.SDK.Entities;
using Zap.WeChat.SDK.IServices;

namespace WeChat.Application
{
    public class CorpAppService : ICorpAppService
    {
        IApplicationConfigService _configService;
        IWeChatAppRepository _repository;

        public CorpAppService(IApplicationConfigService configService, IWeChatAppRepository repository)
        {
            _configService = configService;
            _repository = repository;
        }


        public CorpAppConfig GetCorpApp(string appcode)
        {
            var app = _repository.ListEntities()
                .FirstOrDefault(m => m.AppCode?.ToLower() == appcode?.ToLower());
            return new CorpAppConfig
            {
                CorpId = "wx2e8cc6975a5fa1ce",
                AgentId = app.WeChatAppID,
                AppCode = appcode,
                CorpSecret = app.SecretValue
            };
        }

        public CorpAppConfig GetCorpApp(int agentId)
        {
            throw new System.NotImplementedException();
        }
    }
}
