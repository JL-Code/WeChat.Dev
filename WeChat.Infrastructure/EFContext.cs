using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using WeChat.Domain.SeedWork;
using WeChat.Infrastructure.EntityTypeConfiguration;

namespace WeChat.Infrastructure
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public class EFContext : DbContext, IUnitOfWork
    {
        private EFContext(string connstr)
            : base(connstr)
        {

        }

        /// <summary>
        /// 模型创建前执行
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //modelBuilder.Configurations.Add(new ApplicationConfigEntityTypeConfiguration());
            //modelBuilder.Configurations.Add(new WeChatAppEntityTypeConfiguration());
            //modelBuilder.Configurations.Add(new UserEntityTypeConfiguration());

            var asm = Assembly.Load("WeChat.Infrastructure");
            var typesToRegister = asm.GetTypes()
                        .Where(type => !String.IsNullOrEmpty(type.Namespace))
                        .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configInstance = Activator.CreateInstance(type);
                if (configInstance == null)
                    continue;
                modelBuilder.Configurations.Add(configInstance);
            }
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="connstr"></param>
        /// <returns></returns>
        public static EFContext CreateForEFDesignTools(string connstr)
        {
            return new EFContext(connstr);
        }

        public Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}
