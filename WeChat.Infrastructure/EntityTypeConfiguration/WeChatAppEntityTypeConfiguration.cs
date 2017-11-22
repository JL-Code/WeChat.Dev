using System.Data.Entity.ModelConfiguration;
using WeChat.Domain.AggregatesModel;

namespace WeChat.Infrastructure.EntityTypeConfiguration
{
    public class WeChatAppEntityTypeConfiguration : EntityTypeConfiguration<WeChatAppConfig>
    {
        public WeChatAppEntityTypeConfiguration()
        {
            HasKey(e => e.AppGUID);
            ToTable("sys_WeChatAppConfig");
        }
    }
}
