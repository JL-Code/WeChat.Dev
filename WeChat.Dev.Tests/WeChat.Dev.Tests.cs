using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;
using Zap.WeChat.SDK;
using WeChat.Domain.AggregatesModel;
using WeChat.Infrastructure;
using Zap.WeChat.SDK.Cache;

namespace WeChat.Dev.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AutoFac_Test()
        {
            var connstr = "server=.;database=dev;uid=sa;pwd=123456";
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<WeChatAppService>().As<IWeChatAppService>();
            builder.RegisterType<WeChatAppRepository>().As<IWeChatAppRepository>();
            builder.RegisterInstance(EFContext.CreateForEFDesignTools(connstr));
            //builder.RegisterModule(new ConfigurationSettingsReader("autofac"));
            //using (IContainer container = builder.Build())
            //{
            //    AutoFacManager manager = container.Resolve<AutoFacManager>();
            //    manager.Say();
            //}

            using (IContainer container = builder.Build())
            {
                IWeChatAppService service = container.Resolve<IWeChatAppService>();
                var app = service.GetApp(Constants.MOBILE_APPROVAL);
                Console.WriteLine(app.WeChatAppID);
            }

        }

        [TestMethod]
        public void LocalCacheManager_Add_IsNotNull()
        {
            LocalCacheManager.Add("test", 1212);
            Console.WriteLine(LocalCacheManager.Get<int>("test"));
        }
    }
}
