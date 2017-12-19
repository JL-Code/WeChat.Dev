using System;
using System.Linq;
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
