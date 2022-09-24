using Core.DTOs;
using Core.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BookingTablesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationManager _authenticationManager;
        private readonly ITokenService _tokenService;
        public AuthenticationController(IAuthenticationManager authenticationManager,ITokenService tokenService)
        {
            _authenticationManager = authenticationManager;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
     
        public async Task<IActionResult> Authenticate(UserForAuthenticationDTO userAuth)
        {
          var user =  await _authenticationManager.ValidateUserAsync(userAuth);

          if (user == null)
          {
                return BadRequest();
          }

          string token =_tokenService.CreateToken(user);
           
          return Ok(new { Token = token});

        }
    }
}
