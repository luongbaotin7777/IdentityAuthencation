﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAuthencation.Service.Facebook
{
    public interface IFacebookService
    {
        ChallengeResult FaceBookLogin();

        Task<string> ExternalLoginCallback();
    }
}
