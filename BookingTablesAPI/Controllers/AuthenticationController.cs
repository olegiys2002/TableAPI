using BookingTablesAPI.Filters;
using Core.DTOs;
using Core.IServices;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingTablesAPI.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationManager _authenticationManager;
        public AuthenticationController(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }
        [HttpPost("{login}")]
        [ValidationFilter]
        public async Task<IActionResult> Authenticate(UserForAuthenticationDTO user)
        {
          bool isValid =  await _authenticationManager.ValidateUser(user);
          if (isValid == false)
          {
                return BadRequest();
          }
           string token =_authenticationManager.CreateToken();
           return Ok(token);

        }
    }
}
