using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Threading.Tasks;
using WeChat.Application;
using WeChat.Domain.AggregatesModel;
using WeChat.Infrastructure;

namespace WeChat.Dev.OAuthProviders
{
    /// <summary>
    /// 刷新令牌代理
    /// </summary>
    public class SimpleRefreshTokenProvider : IAuthenticationTokenProvider
    {
        IRefreshTokenService _refreshTokenService;
        public IRefreshTokenService RefreshTokenService => _refreshTokenService ?? AutofacManager.Resolve<IRefreshTokenService>();
        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 生成刷新令牌
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var clientid = context.Ticket.Properties.Dictionary["as:client_id"];

            if (string.IsNullOrEmpty(clientid))
            {
                return;
            }

            var refreshTokenId = Guid.NewGuid().ToString("n");

            var refreshTokenLifeTime = context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime");

            var refreshToken = new RefreshToken()
            {
                Id = HashHanlder.GetHash(refreshTokenId),
                ClientId = clientid,
                Subject = context.Ticket.Identity.Name,//面向的用户
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime))
            };

            context.Ticket.Properties.IssuedUtc = refreshToken.IssuedUtc;
            context.Ticket.Properties.ExpiresUtc = refreshToken.ExpiresUtc;

            refreshToken.ProtectedTicket = context.SerializeTicket();
            //正在构建将保存在RefreshTokens表中的令牌记录，请注意，我正在检查将保存在数据库中的令牌对于此主题（用户）和客户端是唯一的，如果不是唯一的，将删除现有的并存储新的刷新令牌。最好在存储之前对刷新令牌标识符进行散列，因此如果有人访问数据库，他将看不到真正的刷新令牌。
            var result = await RefreshTokenService.AddRefreshTokenAsync(refreshToken);

            if (result)
            {
                context.SetToken(refreshTokenId);
            }

        }

        /// <summary>
        /// 根据刷新令牌生成访问令牌
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            //我们需要通过从Owin Context获取值来设置“Access-Control-Allow-Origin”头，我花了1个多小时，弄清楚为什么我使用刷新令牌发出访问令牌的请求返回405状态代码原来我们需要在这个方法中设置这个头，因为我们设置这个头的方法“GrantResourceOwnerCredentials”永远不会执行一旦我们使用刷新令牌（grant_type = refresh_token）请求访问令牌。
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });
            string hashedTokenId = HashHanlder.GetHash(context.Token);

            var refreshToken = await RefreshTokenService.FindRefreshTokenAsync(hashedTokenId);

            if (refreshToken != null)
            {
                //Get protectedTicket from refreshToken class
                context.DeserializeTicket(refreshToken.ProtectedTicket);
                //我们将从表“RefreshTokens”中删除现有的刷新令牌，因为在我们的逻辑中，我们只允许每个用户和客户端只有一个刷新令牌。
                await RefreshTokenService.RemoveRefreshTokenAsync(hashedTokenId);
            }
        }
    }
}