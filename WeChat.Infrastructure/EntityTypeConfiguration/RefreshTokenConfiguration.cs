using System.Data.Entity.ModelConfiguration;
using WeChat.Domain.AggregatesModel;

namespace WeChat.Infrastructure.EntityTypeConfiguration
{
    public class RefreshTokenConfiguration : EntityTypeConfiguration<RefreshToken>
    {
        public RefreshTokenConfiguration()
        {
            HasKey(e => e.Id);
            ToTable("sys_RefreshToken");
        }

    }
}