using BookingTablesAPI.Filters;
using Core.DTOs;
using Core.IServices;
using Microsoft.AspNetCore.Authorization;
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

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var userDTOs = await _userService.GetUsers();
            return userDTOs == null ? NotFound() : Ok(userDTOs);
        }
        [HttpGet("{id}",Name ="UserById")]
        [ValidationFilter]
        public async Task<IActionResult> GetUserById(int id)
        {
            var userDTO = await _userService.GetUserById(id);
            return userDTO == null? NotFound() : Ok(userDTO);
        }
        [Route("{id}/avatar")]
        [HttpGet]
        public async Task<IActionResult> GetUserAvatar(int id)
        {
           var avatarDTO = await _userService.GetUserAvatar(id);
           return avatarDTO == null ? NotFound() : Ok(avatarDTO);
        }
        [HttpPost]
        [ValidationFilter]
        public async Task<IActionResult> CreateUser([FromForm]UserFormDTO userForCreationDTO)
        {
            var userDTO = await _userService.CreateUser(userForCreationDTO);
            return userDTO == null ? BadRequest() : CreatedAtRoute("UserById",new { userDTO.Id },userDTO);
        }

        [HttpPut("{id}")]
        [ValidationFilter]
        public async Task<IActionResult> UpdateUser(int id , UserFormDTO userForUpdatingDTO)
        {
            var isSuccess = await _userService.UpdateUser(id,userForUpdatingDTO);
            return isSuccess ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        [ValidationFilter]
        public async Task<IActionResult> DeleteUser(int id)
        {
           var isSuccess = await _userService.DeleteUser(id);
           return isSuccess ? NoContent() : NotFound();
        }
    }
}
