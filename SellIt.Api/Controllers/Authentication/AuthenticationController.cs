namespace SellIt.Api.Controllers.Authentication
{
    using Microsoft.AspNetCore.Mvc;
    using SellIt.Api.Contracts.Auth;
    using SellIt.Api.Models;

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
