using WeChat.Domain.AggregatesModel;

namespace WeChat.Infrastructure
{
    public class ApplicationConfigRepository : Repository<ApplicationConfig>, IApplicationConfigRepository
    {
        public ApplicationConfigRepository(EFContext context) : base(context)
        {
        }
    }
}
