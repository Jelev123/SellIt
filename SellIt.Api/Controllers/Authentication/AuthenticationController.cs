namespace SellIt.Api.Controllers.Authentication
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Api.Models;
    using SellIt.Api.Services.Auth;

    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationManager authManager;

        public AuthenticationController(IAuthenticationManager authManager)
        {
            this.authManager = authManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthCredentials credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!await authManager.ValidateCredentials(credentials))
            {
                return Unauthorized();
            }

            return Ok(new { Token = authManager.CreateToken() });

        }
    }
}
