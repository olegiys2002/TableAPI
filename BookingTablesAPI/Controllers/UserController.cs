using BookingTablesAPI.Filters;
using Core.DTOs;
using Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingTablesAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        
        public IActionResult GetUsers()
        {
            List<UserDTO> userDTOs = _userService.GetUsers();
            return Ok(userDTOs);
        }
        [HttpGet("{id}",Name ="UserById")]
        [ValidationFilter]
        public async Task<IActionResult> GetUserById(int id)
        {
            UserDTO userDTO = await _userService.GetUserById(id);
            if (userDTO == null)
            {
                return NotFound();
            }
            return Ok(userDTO);
        }

        [HttpPost]
        [ValidationFilter]
        public async Task<IActionResult> CreateUser(UserForCreationDTO userForCreationDTO)
        {
            UserDTO userDTO = await _userService.CreateUser(userForCreationDTO);
            return CreatedAtRoute("UserById",new { userDTO.Id },userDTO);
        }

        [HttpPut("{id}")]
        [ValidationFilter]
        public async Task<IActionResult> UpdateUser(int id , UserForUpdatingDTO userForUpdatingDTO)
        {
            bool isSuccess = await _userService.UpdateUser(id,userForUpdatingDTO);
            return isSuccess ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        [ValidationFilter]
        public async Task<IActionResult> DeleteUser(int id)
        {
           bool isSuccess = await _userService.DeleteUser(id);
           return isSuccess ? NoContent() : NotFound();
        }
    }
}
