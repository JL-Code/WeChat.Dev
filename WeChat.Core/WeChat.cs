using Senparc.Weixin.Work.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChat.Core
{
    public class WeChat
    {
        /// <summary>
        /// 注册微信配置信息
        /// </summary>
        /// <param name="corpId"></param>
        public static void Register(string corpId)
        {
            AccessTokenContainer.Register("", "");
        }
    }
}
