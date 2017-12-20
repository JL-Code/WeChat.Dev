using System;
using System.Linq;
using System.Threading.Tasks;
using WeChat.Domain.AggregatesModel;

namespace WeChat.Application
{
    public class AccountService : IAccountService
    {
        private IUserRepository _repository;
        public AccountService(IUserRepository repository)
        {
            _repository = repository;
        }

        public void BindWorkID(Guid userid, string workUserId)
        {
            var user = _repository.GetAsync(userid).Result;
            if (user != null)
            {
                user.WorkUserId = workUserId;
            }
            _repository.Update(user);
        }

        public Task<User> FindUserAsync(string account, string password)
        {
            return _repository.GetAsync(m => m.Account == account && m.Password == password);
        }

        public User FindUserByWxUserID(string wxuserId)
        {
            var user = _repository.ListEntities().FirstOrDefault(m => m.WorkUserId == wxuserId);
            return user;
        }

        public User Login(string account, string password)
        {
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
                throw new ArgumentNullException($"{nameof(account)}或{nameof(password)}");
            var user = _repository.ListEntities().FirstOrDefault(m => m.Account == account && m.Password == password);
            if (user == null)
                throw new Exception("用户名或密码不正确");
            return user;
        }
    }
}
