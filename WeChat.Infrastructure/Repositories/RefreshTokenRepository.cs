using WeChat.Domain.AggregatesModel;

namespace WeChat.Infrastructure
{
    public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
    {

        public RefreshTokenRepository(EFContext context) : base(context)
        {

        }
    }
}