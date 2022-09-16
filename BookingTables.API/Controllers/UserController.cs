using BookingTablesAPI.Filters;
using Core.DTOs;
using Core.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingTablesAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var userDTOs = await _userService.GetUsersAsync();
            return userDTOs == null ? NotFound() : Ok(userDTOs);
        }

        [HttpGet("{id}",Name ="UserById")]
        [ValidationFilter]
        public async Task<IActionResult> GetUserById(int id)
        {
            var userDTO = await _userService.GetUserByIdAsync(id);
            return userDTO == null? NotFound() : Ok(userDTO);
        }

        [Route("{id}/avatar")]
        [HttpGet]
        public async Task<IActionResult> GetUserAvatar(int id)
        {
           var avatarDTO = await _userService.GetUserAvatarAsync(id);
           return avatarDTO == null ? NotFound() : Ok(avatarDTO);
        }

        [HttpPost]
        [ValidationFilter]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromForm]UserFormDTO trys)
        {
            var userDTO = await _userService.CreateUserAsync(trys);
            return userDTO == null ? BadRequest() : CreatedAtRoute("UserById",new { userDTO.Id },userDTO);
        }

        [HttpPut("{id}")]
        [ValidationFilter]
        public async Task<IActionResult> UpdateUser(int id ,[FromForm] UserFormDTO userForUpdatingDTO)
        {
            var updatedUser = await _userService.UpdateUserAsync(id,userForUpdatingDTO);
            return updatedUser == null ? Ok(updatedUser) : NotFound();
        }

        [HttpDelete("{id}")]
        [ValidationFilter]
        public async Task<IActionResult> DeleteUser(int id)
        {
           var userId = await _userService.DeleteUserAsync(id);
           return userId == null ? Ok(userId) : NotFound();
        }
    }
}
