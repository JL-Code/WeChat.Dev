using System;
using System.Threading.Tasks;
using WeChat.Domain.AggregatesModel;

namespace WeChat.Application
{
    public interface IAccountService
    {
        User Login(string account, string password);

        User FindUserByWxUserID(string wxuserId);

        void BindWorkID(Guid userid, string workUserId);

        Task<User> FindUserAsync(string account, string password);
    }
}
