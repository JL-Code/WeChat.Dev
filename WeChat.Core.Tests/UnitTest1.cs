using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;
using WeChat.Domain.AggregatesModel;
using WeChat.Infrastructure;
using WeChat.Core.Implementation;
using WeChat.Core.Cache;

namespace WeChat.Core.Tests
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
            builder.RegisterType<MessageService>().As<IMessageService>();
            builder.RegisterType<MemberService>().As<IMemberService>();
            builder.RegisterType<WeChatAppRepository>().As<IWeChatAppRepository>();
            builder.RegisterInstance(EFContext.CreateForEFDesignTools(connstr));
            container = builder.Build();

            
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
    }
}
