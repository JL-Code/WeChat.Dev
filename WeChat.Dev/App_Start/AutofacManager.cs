﻿using Autofac;
using System.Linq;
using System.Reflection;
using WeChat.Domain.SeedWork;
using WeChat.Infrastructure;

namespace WeChat.Dev
{
    /// <summary>
    /// 非http请求生成实例的 autofac容器管理
    /// </summary>
    public class AutofacManager
    {
        const string connstr = "server=.;database=dev;uid=sa;pwd=123456";
        private static IContainer _autofacContainer;
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
            builder.Register(m => EFContext.CreateForEFDesignTools(connstr)).AsSelf().InstancePerDependency();
            _autofacContainer = builder.Build();
        }

        private static void Register(string path, string suffix, ContainerBuilder builder)
        {
            var assembly = Assembly.Load(path);
            builder.RegisterAssemblyTypes(assembly)
                .Where(m => m.Name.EndsWith(suffix))
                .AsImplementedInterfaces()//指定将一个类型注册为提供其所有实现的接口
                .InstancePerDependency();//当被依赖的时候 生成新的一个唯一的实例
        }

        #endregion
    }
}