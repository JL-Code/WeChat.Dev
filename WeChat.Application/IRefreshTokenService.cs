using System.Threading.Tasks;
using WeChat.Domain.AggregatesModel;

namespace WeChat.Application
{
    public interface IRefreshTokenService
    {

        Task<bool> AddRefreshTokenAsync(RefreshToken refreshToken);

        Task<RefreshToken> FindRefreshTokenAsync(string hashedTokenId);

        Task<bool> RemoveRefreshTokenAsync(string hashedTokenId);
    }
}
