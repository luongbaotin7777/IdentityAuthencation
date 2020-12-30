using IdentityAuthencation.Dtos;
using IdentityAuthencation.Entities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityAuthencation.Service.Token
{
    public interface ITokenService
    {
        string GenerateJWTToken(ApplicationUser User, int expMinute);
        RefreshToken GenerateRefreshJWTToken(ApplicationUser User);
    }
}
