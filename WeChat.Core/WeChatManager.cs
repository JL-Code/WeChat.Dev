using Senparc.Weixin.Work.Containers;
using System;
using Zap.WeChat.SDK.Entities;

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


        public static void RegisterWorkApp(string appcode, CorpAppConfig app)
        {
            throw new NotImplementedException();
        }
    }
}
