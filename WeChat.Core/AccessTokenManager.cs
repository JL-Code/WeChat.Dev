using Senparc.Weixin.Work.Containers;

namespace Zap.WeChat.SDK
{
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
    }
}
