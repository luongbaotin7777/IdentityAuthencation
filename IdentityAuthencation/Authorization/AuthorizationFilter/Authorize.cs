using IdentityAuthencation.Service.Token;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace IdentityAuthencation.Authorization.AuthorizationFilter
{
    public class Authorize : Attribute, IAuthorizationFilter
    {
        private readonly ITokenService _tokenService;

        public Authorize(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var Authorization = context.HttpContext.Request.Headers["Authorization"];

        }
    }
}
