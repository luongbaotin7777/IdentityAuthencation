
using IdentityAuthencation.Dtos;
using IdentityAuthencation.Entities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityAuthencation.Service.Interface
{
    public interface ITokenService
    {
        Task<string> GenerateJWTToken(ApplicationUser User, int expMinute);
        RefreshToken GenerateRefreshJWTToken(ApplicationUser User);
    }
}
