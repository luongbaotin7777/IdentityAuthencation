using IdentityAuthencation.Entities;
using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IdentityAuthencation.Dtos
{
    public class AuthenticateResponseDto
    {
        private ApplicationUser userName;
        private Task<string> jwtToken;
        private string token;

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public Task<string> JwtToken { get; set; }

        [JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }

        public AuthenticateResponseDto(ApplicationUser user, Task<string> jwtToken, string refreshToken)
        {
            this.Id = user.Id;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Username = user.UserName;
            this.JwtToken = jwtToken;
            this.RefreshToken = refreshToken;
        }

    }
}
