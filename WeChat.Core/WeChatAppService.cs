using System;
using System.Linq;
using WeChat.Domain.AggregatesModel;

namespace WeChat.Core
{
    public class WeChatAppService : IWeChatAppService
    {
        IWeChatAppRepository _repository;
        public WeChatAppService(IWeChatAppRepository repository)
        {
            _repository = repository;
        }
        public WeChatAppConfig GetApp(string appCode)
        {
            var entity = _repository.ListEntities().Where(m => m.AppCode?.ToLower() == appCode?.ToLower()).FirstOrDefault();
            return entity;
        }
    }
}
