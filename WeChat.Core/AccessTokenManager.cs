using Senparc.Weixin.Work.Containers;

namespace WeChat.Core
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
    }
}
