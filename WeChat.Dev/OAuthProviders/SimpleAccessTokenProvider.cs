using Microsoft.Owin.Security.Infrastructure;
using System.Threading.Tasks;

namespace WeChat.Dev.OAuthProviders
{
    /// <summary>
    /// 认证服务代理 用于生成访问令牌
    /// </summary>
    public class SimpleAccessTokenProvider : AuthenticationTokenProvider
    {
        public override Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            return base.CreateAsync(context);
        }

        public override Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            return base.ReceiveAsync(context);
        }
    }
}