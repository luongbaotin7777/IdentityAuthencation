using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityAuthencation.Helpers;
using IdentityAuthencation.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityAuthencation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacebookController : ControllerBase
    {
        private readonly IFacebookService _facebookService;
        public FacebookController(IFacebookService facebookService)
        {
            _facebookService = facebookService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("/api/facebook-login")]
        public IActionResult FacebookLogin()
        {
            return _facebookService.FaceBookLogin();

        }
        [AllowAnonymous]
        [HttpGet]
        [Route("/api/signin-facebook")]
        public async Task<IActionResult> ExternalLoginCallback()
        {
            try
            {
                var token = await _facebookService.ExternalLoginCallback();

                return Ok(token);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
