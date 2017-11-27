using Senparc.Weixin.Work.Containers;

namespace Zap.WeChat.SDK
{
    /// <summary>
    /// 访问令牌管理
    /// </summary>
    public class AccessTokenManager
    {
        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="corpSecret"></param>
        /// <param name="getNewToken"></param>
        /// <returns></returns>
        public static string GetToken(string corpId, string corpSecret, bool getNewToken = false)
        {
            var appKey = AccessTokenContainer.BuildingKey(corpId, corpSecret);
            var result = AccessTokenContainer.GetToken(appKey, getNewToken);
            return result;
        }

        /// <summary>
        /// 使用完整的应用凭证获取Token，如果不存在将自动注册
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="corpSecret"></param>
        /// <param name="getNewToken"></param>
        /// <returns></returns>
        public static string TryGetToken(string corpId, string corpSecret, bool getNewToken = false)
        {
            var result = AccessTokenContainer.TryGetToken(corpId, corpSecret, getNewToken);
            return result;
        }
        /// <summary>
        /// 注册应用凭证信息，此操作只是注册，不会马上获取Token，并将清空之前的Token。 执行此注册过程，会连带注册ProviderTokenContainer。
        /// </summary>
        /// <param name="corpId">企业ID</param>
        /// <param name="corpSecret">企业应用秘钥</param>
        /// <returns></returns>
        public static string BuildingKey(string corpId, string corpSecret)
        {
            return AccessTokenContainer.BuildingKey(corpId, corpSecret);
        }
    }
}
