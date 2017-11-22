using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using System.Web.Mvc;
using WeChat.Infrastructure;

namespace WeChat.Dev
{
    public class AutofacConfig
    {
        const string connstr = "server=.;database=dev;uid=sa;pwd=123456";
        public static void RegisterAutofac()
        {
            var builder = new ContainerBuilder();

            var maps = new string[] { "WeChat.Core", "WeChat.Infrastructure" };
            Register(maps[0], "Service", builder);
            Register(maps[1], "Repository", builder);
            builder.Register(m => EFContext.CreateForEFDesignTools(connstr)).InstancePerRequest();
            RegisterMvc(builder);
        }

        private static void Register(string path, string suffix, ContainerBuilder builder)
        {
            var assembly = Assembly.Load(path);
            builder.RegisterAssemblyTypes(assembly)
                .Where(m => m.Name.EndsWith(suffix))
                .AsImplementedInterfaces()//默认实现接口
                .InstancePerRequest();//每次http请求生成一个实例
        }


        private static void RegisterMvc(ContainerBuilder builder)
        {
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            //设置依赖解析
            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
        }
    }
}