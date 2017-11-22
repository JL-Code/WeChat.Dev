using WeChat.Domain.AggregatesModel;

namespace WeChat.Infrastructure.Repositories
{
    public class WeChatAppRepository : Repository<WeChatAppConfig>, IWeChatAppRepository
    {
        public WeChatAppRepository(EFContext context) : base(context)
        {
        }
    }
}
