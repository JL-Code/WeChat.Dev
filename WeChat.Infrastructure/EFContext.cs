using System;
using System.Data.Entity;
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
            modelBuilder.Configurations.Add(new ApplicationConfigEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new WeChatAppEntityTypeConfiguration());
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
