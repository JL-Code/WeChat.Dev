﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;
using WeChat.Domain.AggregatesModel;
using WeChat.Infrastructure;
using Zap.WeChat.SDK.Cache;
using Zap.WeChat.SDK.IServices;
using System.Collections.Generic;
using System.Linq;
using Zap.WeChat.SDK.Handler;
using WeChat.Application;
using Zap.WeChat.SDK.Helpers;
using System.IO;
using System.Drawing;

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
            AddCache();
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

        [TestMethod]
        public void GetDynamicCache_Dynamic_DynamicIsNotNull()
        {
            var obj = LocalCacheManager.Get<dynamic>("dynamic");
            Console.WriteLine(obj.userid);
            Console.WriteLine(obj.pwd);
        }

        private static void AddCache()
        {
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long timeStamp = (long)(DateTime.Now - startTime).TotalSeconds;
            LocalCacheManager.Add("dynamic", new { userid = "jiangy@highzap.com", pwd = "123456", uid = "jiangy", exp_in = timeStamp });
        }

        [TestMethod]
        public void Test_JSSDK_Valid()
        {
            var appService = container.Resolve<ICorpAppService>();
            var handler = new JSSDKHandler(appService, Constants.MOBILE_APPROVAL);
            var url1 = @"http://meunsc.oicp.net/dev/ma/flowform/formdetail?id=a353688a-d3d8-e711-9708-8cdcd450dc4b&nodeid=797c409a-d3d8-e711-9708-8cdcd450dc4b&userId=bf632547-f432-e611-963d-c35f2f517ea0&nodeExtType=3&nodeExtGuid=6225e0c4-d85a-4dbd-8a18-43e898ce3755&src=/dev/MA/backlog/mybacklog";

            var url2 = @"http://meunsc.oicp.net/dev/ma/flowform/formdetail?id=a353688a-d3d8-e711-9708-8cdcd450dc4b&nodeid=797c409a-d3d8-e711-9708-8cdcd450dc4b&userId=bf632547-f432-e611-963d-c35f2f517ea0&nodeExtType=3&nodeExtGuid=6225e0c4-d85a-4dbd-8a18-43e898ce3755&src=/dev/MA/backlog/mybacklog";

            var result1 = handler.GetSignature(url1);

            var signature = JSSDKHelper.GetSignature(result1.JsapiTicket, result1.Noncestr, result1.Timestamp, url2);
            Assert.AreEqual(result1.Signature, signature);
        }

        [TestMethod]
        public void Get_Image_WidthHeight()
        {
            using (FileStream fs = new FileStream(@"E:\00_Workspace\Company\Zap_WeChat_SDK\WeChat.Dev\WeChat.Core.Tests\Images\2-1.png", FileMode.Open, FileAccess.Read))
            {
                Image image = Image.FromStream(fs);
                var width = image.Width;
                var height = image.Height;
                Console.WriteLine($"{width}x{height}");
            }
        }
    }
}
