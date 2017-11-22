﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;
using WeChat.Core;
using WeChat.Domain.AggregatesModel;
using WeChat.Infrastructure;
using WeChat.Infrastructure.Repositories;

namespace WeChat.Dev.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
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
    }
}