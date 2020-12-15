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
    public class GoogleController : ControllerBase
    {
        private readonly IGoogleService _googleService;
        public GoogleController(IGoogleService googleService)
        {
            _googleService = googleService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("/api/google-login")]
        public IActionResult GoogleLogin()
        {
            return _googleService.GoogleLogin();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("/api/signin-google")]
        public async Task<IActionResult> ExternalLoginCallback()
        {
            try
            {
                var token = await _googleService.ExternalLoginCallback();

                return Ok(token);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
