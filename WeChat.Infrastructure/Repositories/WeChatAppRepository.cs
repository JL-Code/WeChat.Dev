using WeChat.Domain.AggregatesModel;

namespace WeChat.Infrastructure
{
    public class WeChatAppRepository : Repository<WeChatAppConfig>, IWeChatAppRepository
    {
        public WeChatAppRepository(EFContext context) : base(context)
        {
        }
    }
}
