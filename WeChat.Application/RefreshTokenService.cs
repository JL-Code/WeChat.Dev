using System;
using System.Threading.Tasks;
using WeChat.Domain.AggregatesModel;

namespace WeChat.Application
{
    public class RefreshTokenService : IRefreshTokenService
    {
        IRefreshTokenRepository _repository;
        public RefreshTokenService(IRefreshTokenRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> AddRefreshTokenAsync(RefreshToken refreshToken)
        {
            _repository.Add(refreshToken);
            return await _repository.UnitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<RefreshToken> FindRefreshTokenAsync(string hashedTokenId)
        {
            return await _repository.GetAsync(hashedTokenId);
        }

        public async Task<bool> RemoveRefreshTokenAsync(string hashedTokenId)
        {
            var refreshToken = await FindRefreshTokenAsync(hashedTokenId);

            if (refreshToken != null)
            {
                _repository.Remove(refreshToken);
                return await _repository.UnitOfWork.SaveChangesAsync() > 0;
            }

            return false;
        }
    }
}
