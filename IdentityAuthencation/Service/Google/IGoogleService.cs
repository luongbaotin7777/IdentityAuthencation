using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAuthencation.Service.Google
{
    public interface IGoogleService
    {
        ChallengeResult GoogleLogin();

        Task<string> ExternalLoginCallback();

    }
}
