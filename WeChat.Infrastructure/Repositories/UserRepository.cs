using WeChat.Domain.AggregatesModel;

namespace WeChat.Infrastructure
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(EFContext context) : base(context)
        {
        }
    }
}
