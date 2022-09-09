using BookingTablesAPI.Filters;
using Core.DTOs;
using Core.IServices;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingTablesAPI.Controllers
{
    [Route("api/authentication")]
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
        [ValidationFilter]
        public async Task<IActionResult> Authenticate(UserForAuthenticationDTO userAuth)
        {
          User user =  await _authenticationManager.ValidateUser(userAuth);
          if (user == null)
          {
                return BadRequest();
          }
           string token =_tokenService.CreateToken(user);
           
           return Ok(new { Token = token});

        }
    }
}
