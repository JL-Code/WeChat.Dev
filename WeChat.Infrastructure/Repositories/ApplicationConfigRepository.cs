using WeChat.Domain.AggregatesModel;

namespace WeChat.Infrastructure.Repositories
{
    public class ApplicationConfigRepository : Repository<ApplicationConfig>, IApplicationConfigRepository
    {
        public ApplicationConfigRepository(EFContext context) : base(context)
        {
        }
    }
}
