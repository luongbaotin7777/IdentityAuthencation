using IdentityAuthencation.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAuthencation.Service.Interface
{
    public interface ITokenService
    {
        Task<string> GenerateJWTToken(string UserName, int expDay);
    }
}
