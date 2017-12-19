using System.Data.Entity.ModelConfiguration;
using WeChat.Domain.AggregatesModel;

namespace WeChat.Infrastructure.EntityTypeConfiguration
{
    public class UserEntityTypeConfiguration : EntityTypeConfiguration<User>
    {
        public UserEntityTypeConfiguration()
        {
            HasKey(e => e.UserId);
            ToTable("sys_User");
        }
    }
}
