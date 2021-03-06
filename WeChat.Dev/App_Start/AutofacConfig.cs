﻿using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using WeChat.Domain.SeedWork;
using WeChat.Infrastructure;

namespace WeChat.Dev
{
    public class AutofacConfig
    {
        const string connstr = "server=.;database=dev;uid=sa;pwd=123456";
        static IContainer _autofacContainer;

        /// <summary>
        /// autofac容器
        /// </summary>
        public static IContainer AutofacContainer { get => _autofacContainer; }

        #region Resolve

        public static T Resolve<T>()
        {
            return AutofacContainer.Resolve<T>();
        }

        #endregion

        #region Register

        public static void RegisterAutofac()
        {
            var builder = new ContainerBuilder();
            var maps = new string[] { "Zap.WeChat.SDK", "WeChat.Application", "WeChat.Infrastructure" };
            Register(maps[0], "Service", builder);
            Register(maps[1], "Service", builder);
            Register(maps[2], "Repository", builder);
            builder.Register(m => EFContext.CreateForEFDesignTools(connstr)).AsSelf().As<IUnitOfWork>().InstancePerRequest();
            RegisterMvcAndWebApi(builder);
        }

        private static void Register(string path, string suffix, ContainerBuilder builder)
        {
            var assembly = Assembly.Load(path);
            builder.RegisterAssemblyTypes(assembly)
                .Where(m => m.Name.EndsWith(suffix))
                .AsImplementedInterfaces()//指定将一个类型注册为提供其所有实现的接口
                .InstancePerRequest();//每次http请求生成一个实例
        }

        private static void RegisterMvcAndWebApi(ContainerBuilder builder)
        {
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            //设置依赖解析
            _autofacContainer = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(_autofacContainer));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(_autofacContainer);
        }

        #endregion
    }
}