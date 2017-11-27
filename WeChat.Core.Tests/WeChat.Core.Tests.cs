using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;
using WeChat.Domain.AggregatesModel;
using WeChat.Infrastructure;
using Zap.WeChat.SDK.Implementation;
using Zap.WeChat.SDK.Cache;
using Zap.WeChat.SDK.IServices;
using System.Collections.Generic;
using System.Linq;
using Zap.WeChat.SDK.Handler;

namespace Zap.WeChat.SDK.Tests
{
    [TestClass]
    public class UnitTest1
    {
        IContainer container;

        [TestInitialize]
        public void Initialize()
        {
            LocalCacheManager.Add(Constants.CORP_ID, "wx2e8cc6975a5fa1ce");
            var connstr = "server=.;database=dev;uid=sa;pwd=123456";
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<WeChatAppService>().As<IWeChatAppService>();
            builder.RegisterType<ApplicationConfigService>().As<IApplicationConfigService>();
            builder.RegisterType<MemberService>().As<IMemberService>();
            builder.RegisterType<WeChatAppRepository>().As<IWeChatAppRepository>();
            builder.RegisterType<ApplicationConfigRepository>().As<IApplicationConfigRepository>();
            builder.RegisterType<CorpAppService>().As<ICorpAppService>();
            builder.RegisterInstance(EFContext.CreateForEFDesignTools(connstr));
            container = builder.Build();
            Register();
        }


        public void Register()
        {
            IWeChatAppService appService = container.Resolve<IWeChatAppService>();
            IApplicationConfigService configService = container.Resolve<IApplicationConfigService>();
            var config = configService.ListApplicationConfig()
                .FirstOrDefault(m => m.ConfigType.ToLower() == Constants.WECHAT.ToLower() && m.ConfigKey.ToLower() == Constants.CORP_ID.ToLower());
            LocalCacheManager.Add(Constants.CORP_ID, config.ConfigValue);

            List<WeChatAppConfig> apps = appService.ListApps();
            apps.ForEach(app =>
            {
                WeChatManager.RegisterWorkApp(config.ConfigValue, app.SecretValue, app.AppName);
            });
        }

        [TestMethod]
        public void GetMember_UserIdAndAppCode_NotNull()
        {
            IMemberService memberService = container.Resolve<IMemberService>();
            var member = memberService.GetMember(Constants.MOBILE_APPROVAL, "jiangy@highzap.com");
            Assert.IsNotNull(member);
            Console.WriteLine(member.Name);
        }

        [TestMethod]
        public void GetTicket_AppCode_NotNull()
        {
            IWeChatAppService appService = container.Resolve<IWeChatAppService>();
            var app = appService.GetApp(Constants.MOBILE_APPROVAL);
            var ticket = JsApiTicketManager.TryGetTicket("wx2e8cc6975a5fa1ce", app.SecretValue);
            Assert.IsNotNull(ticket);
            Console.WriteLine(ticket);
        }

        [TestMethod]
        public void SendText_AppCode_CorpAppConfigService_IvalidIsNull()
        {
            var appService = container.Resolve<ICorpAppService>();
            var msgHandler = new MessageHandler(appService, Constants.MOBILE_APPROVAL);
            msgHandler.SendText("你好 我封装了消息发送接口 调用者在实例化消息处理程序时需传入ICorpAppService的实例和应用编码用于获取应用配置信息", "jiangy@highzap.com");
        }

        [TestMethod]
        public void SendNews_AppCode_CorpAppConfigService_IvalidIsNull()
        {
            var appService = container.Resolve<ICorpAppService>();
            var msgHandler = new MessageHandler(appService, Constants.MOBILE_APPROVAL);
            msgHandler.SendNews(new MessageAPI.NewsBody
            {
                Articles = new List<MessageAPI.Article>
                {
                    new MessageAPI.Article{
                        Title = "消息发送测试",
                        Description = "你好我是news消息",
                        Url ="http://www.baidu.com"
                    }
                }
            }, "jiangy@highzap.com");
        }

    }
}
