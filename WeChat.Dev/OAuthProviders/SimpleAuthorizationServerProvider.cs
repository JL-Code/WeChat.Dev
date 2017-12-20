using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WeChat.Application;

namespace WeChat.Dev.OAuthProviders
{
    /// <summary>
    /// 授权服务代理 继承OAuthAuthorizationServerProvider（包含4种认证授权方式） 
    /// </summary>
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        IAccountService _accountService;
        public IAccountService AccountService => _accountService ?? AutofacManager.Resolve<IAccountService>();

        /// <summary>
        /// 验证客户端身份
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {

            string clientId = string.Empty;
            string clientSecret = string.Empty;
            //Client client = null;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                //Remove the comments from the below line context.SetError, and invalidate context 
                //if you want to force sending clientId/secrects once obtain access tokens. 
                context.Validated();
                //context.SetError("invalid_clientId", "ClientId should be sent.");
                return Task.FromResult<object>(null);
            }

            //using (AuthRepository _repo = new AuthRepository())
            //{
            //    client = _repo.FindClient(context.ClientId);
            //}

            //if (client == null)
            //{
            //    context.SetError("无效的客户端信息", string.Format("Client '{0}' is not registered in the system.", context.ClientId));
            //    return Task.FromResult<object>(null);
            //}

            //bs端 无法做到 只针对cs端
            //if (client.ApplicationType == ApplicationTypeEnum.NativeConfidential)
            //{
            //    if (string.IsNullOrWhiteSpace(clientSecret))
            //    {
            //        context.SetError("无效的客户端信息", "Client secret should be sent.");
            //        return Task.FromResult<object>(null);
            //    }
            //    else
            //    {
            //        if (client.Secret != Helper.GetHash(clientSecret))
            //        {
            //            context.SetError("无效的客户端信息", "Client secret is invalid.");
            //            return Task.FromResult<object>(null);
            //        }
            //    }
            //}

            //if (!client.Active)
            //{
            //    context.SetError("无效的客户端信息", "Client is inactive.");
            //    return Task.FromResult<object>(null);
            //}
            //在Owin上下文中存储客户端允许的起始和刷新令牌生命周期值，以便在生成刷新令牌并设置其到期时间后可用。
            //context.OwinContext.Set("as:clientAllowedOrigin", client.AllowedOrigin);
            //context.OwinContext.Set("as:clientRefreshTokenLifeTime", client.RefreshTokenLifeTime.ToString());

            context.Validated();
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// “GrantResourceOwnerCredentials”负责验证发送到授权服务器的令牌端点的用户名和密码，
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //从Owin上下文读取该客户端的允许原始值，然后使用此值将头“Access-Control-Allow-Origin”添加到Owin上下文响应中，通过执行此操作以及我们将阻止使用的任何JavaScript应用程序相同的客户端ID来构建另一个域上托管的JavaScript应用程序; 因为来自此应用程序的所有请求的来源将来自不同的域，后端API将返回405状态。
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");

            if (allowedOrigin == null) allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });
            //如果是这样，我们将检查资源所有者的用户名/密码，如果是这种情况，我们将为此用户生成一组声明以及包含客户端id和userName的身份验证属性，这些属性需要接下来的步骤。
            var user = await _accountService.FindUserAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("无效的授权", "用户名或密码无效。");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            #region 为此用户生成一组声明以及包含客户端id和userName的身份验证属性，这些属性需要接下来的步骤。(需要封装)

            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim("isadmin", "True"));
            identity.AddClaim(new Claim("name", "mecode"));
            identity.AddClaim(new Claim("company", "衡泽科技"));

            #endregion

            var props = new AuthenticationProperties(new Dictionary<string, string>{
                {"as:client_id", context.ClientId ?? string.Empty }
            });
            var ticket = new AuthenticationTicket(identity, props);
            //调用“context.Validated（ticket）”时，会在幕后生成访问令牌
            context.Validated(ticket);

        }

        /// <summary>
        /// 授予刷新令牌
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
            var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.SetError("无效的客户端信息", "刷新令牌是来自一个不同的客户ID。");
                return Task.FromResult<object>(null);
            }
            // Change auth ticket for refresh token requests 改变授权票刷新令牌的请求
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// 令牌生成成功的最后阶段调用 可以添加额外的参数返回给客户端
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            //添加额外的key-value到token 的响应中
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return Task.FromResult<object>(null);
        }

    }
}