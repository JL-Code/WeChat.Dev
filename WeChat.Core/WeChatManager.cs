using Senparc.Weixin.Work.Containers;
using Zap.WeChat.SDK.Cache;

namespace Zap.WeChat.SDK
{
    public class WeChatManager
    {
        /// <summary>
        /// 注册微信配置信息
        /// </summary>
        /// <param name="corpId">企业ID</param>
        /// <param name="corpSecret">企业秘钥或应用秘钥</param>
        /// <param name="name">应用名</param>
        public static void RegisterWorkApp(string corpId, string corpSecret, string name = null)
        {
            AccessTokenContainer.Register(corpId, corpSecret, name);
        }

        /// <summary>
        /// 注册CoropId
        /// </summary>
        /// <param name="corpId"></param>
        public static void RegisterCoprID(string corpId)
        {
            LocalCacheManager.Add(Constants.CORP_ID, corpId);
        }

        /// <summary>
        /// 重新注册CorpId
        /// </summary>
        /// <param name="corpId"></param>
        public static void ReRegisterCoprID(string corpId)
        {
            LocalCacheManager.Update(Constants.CORP_ID, corpId);
        }
    }
}
