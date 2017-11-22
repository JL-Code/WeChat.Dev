using System.Data.Entity.ModelConfiguration;
using WeChat.Domain.AggregatesModel;

namespace WeChat.Infrastructure.EntityTypeConfiguration
{
    public class ApplicationConfigEntityTypeConfiguration : EntityTypeConfiguration<ApplicationConfig>
    {
        public ApplicationConfigEntityTypeConfiguration()
        {
            HasKey(e => e.ConfigGUID);
            ToTable("sys_InterfaceConfig");
        }
    }
}
