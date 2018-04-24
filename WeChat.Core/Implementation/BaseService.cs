using System;
using Zap.WeChat.SDK.Entities;

namespace Zap.WeChat.SDK.IServices
{
    public class BaseService
    {
        private ICorpAppService _appService;
        private readonly string _appcode;
        private readonly int _agentId;
        private CorpAppConfig _appConfig = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="appcode"></param>
        public BaseService(ICorpAppService service, string appcode)
        {
            _appService = service;
            _appcode = appcode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="agentId"></param>
        public BaseService(ICorpAppService service, int agentId)
        {
            _appService = service;
            _agentId = agentId;
        }

        /// <summary>
        /// 应用配置信息
        /// </summary>
        protected CorpAppConfig AppConfig
        {
            get
            {
                if (!string.IsNullOrEmpty(_appcode))
                {
                    _appConfig = _appService.GetCorpApp(_appcode);
                }
                else if (_agentId != 0)
                {
                    _appConfig = _appService.GetCorpApp(_agentId);
                }
                else
                {
                    throw new ArgumentNullException($"{nameof(_appcode)}或{nameof(_agentId)}");
                }
                return _appConfig;
            }
        }

        /// <summary>
        /// AppKey
        /// </summary>
        protected string AppKey
        {
            get
            {
                return AccessTokenManager.BuildingKey(AppConfig.CorpId, AppConfig.Secret);
            }
        }
    }
}
