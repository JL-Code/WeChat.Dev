using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WeChat.Dev.OAuthProviders;

[assembly: OwinStartup(typeof(WeChat.Dev.Startup))]

namespace WeChat.Dev
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkID=316888
            var connstr = WebConfigurationManager.ConnectionStrings["devConnstr"].ToString();
            AreaRegistration.RegisterAllAreas();

            GlobalConfiguration.Configure(WebApiConfig.Register);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutofacConfig.RegisterAutofac();
            AutofacManager.RegisterAutofac();
            //企业微信应用信息注册
            AccessTokenConfig.Register();

            ConfigureOAuth(app);
        }

        /// <summary>
        /// 配置OAuth认证授权相关
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureOAuth(IAppBuilder app)
        {
            //生成令牌的路径将是：“http：// localhost：port / token”。我们将看到我们将如何在后续步骤中发出HTTP POST请求以生成令牌。
            //我们已经指定了如何验证用户要求在名为“SimpleAuthorizationServerProvider”的自定义类中的令牌的凭据的实现。
            //选项类提供控制授权服务器中间件行为所需的信息
            var OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                //[true]时 允许授权和令牌请求到达HTTP URI地址，并且允许传入redirect_uri授权请求参数有HTTP URI地址。
                AllowInsecureHttp = true,
                //客户端应用程序直接通过 OAuth 协议与之通信的请求路径。 必须以前导斜杠开头，如“/Token”。 如果为客户端颁发了 client_secret，则必须将其提供给此终结点。
                TokenEndpointPath = new PathString("/api/token"),
                //授权路径
                AuthorizeEndpointPath = new PathString("/api/authorize"),
                //设置令牌过期时间
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(10),
                //令牌授权服务 用于处理授权服务器中间件引发的事件
                Provider = new SimpleAuthorizationServerProvider(),
                //认证服务代理
                AccessTokenProvider = new SimpleAccessTokenProvider(),
                //用于生成刷新令牌的代理
                RefreshTokenProvider = new SimpleRefreshTokenProvider()
            };
            //向 OWIN Ｗeb 应用程序添加 OAuth2 授权服务器功能。 此中间件执行由 OAuth2 规范定义的授权和令牌终结点请求处理
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            //生成 OWIN 应用程序的 OAuth 持有者身份验证。
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
