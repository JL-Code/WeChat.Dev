using WeChat.Domain.AggregatesModel;

namespace WeChat.Application
{
    public interface IAccountService
    {
        User Login(string account, string password);

        User FindUserByWxUserID(string wxuserId);
    }
}
