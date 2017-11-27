using Zap.WeChat.SDK.Entities;

namespace Zap.WeChat.SDK.IServices
{
    /// <summary>
    /// 开发者实现 企业应用接口
    /// </summary>
    public interface ICorpAppService
    {
        /// <summary>
        /// 获取企业应用配置信息
        /// </summary>
        /// <param name="appcode">应用编码</param>
        /// <returns></returns>
        CorpAppConfig GetCorpApp(string appcode);

        /// <summary>
        /// 获取企业应用配置信息
        /// </summary>
        /// <param name="agentId">企业应用ID</param>
        /// <returns></returns>
        CorpAppConfig GetCorpApp(int agentId);
    }
}
