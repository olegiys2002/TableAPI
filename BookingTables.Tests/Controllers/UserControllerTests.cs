using BookingTablesAPI.Controllers;
using Core.DTOs;
using Core.IServices;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Nest;
using Shared.RequestModels;
using Xunit;

namespace API.Tests
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _userService = new ();
        private readonly Mock<UserRequest> userRequest = new ();
        private readonly Mock<IElasticClient> elasticClient = new Mock<IElasticClient> ();
        [Fact]
        public async Task UserController_GetUsers_ReturnOk()
        {

            var usersDTOs = new Mock<List<UserDTO>>();
            
            _userService.Setup(us => us.GetUsersAsync(userRequest.Object)).Returns(Task.FromResult(usersDTOs.Object));
            var userController = new UserController(_userService.Object,elasticClient.Object);

            var result = await userController.GetUsers(userRequest.Object);

            Assert.IsType<OkObjectResult>(result as OkObjectResult);
        }

        [Fact]
        public async Task UserController_GetUsers_NotFound()
        {
            var usersDTOs = new List<UserDTO>();
            usersDTOs = null;
            _userService.Setup(us => us.GetUsersAsync(userRequest.Object)).Returns(Task.FromResult(usersDTOs));
            var userController = new UserController(_userService.Object, elasticClient.Object);

            var result = await userController.GetUsers(userRequest.Object);

            Assert.IsType<NotFoundResult>(result as NotFoundResult);
        }

        [Fact]

        public async Task UserController_GetUserById_Ok()
        {
            var id = new int();
            UserDTO userDTO = new UserDTO();
            _userService.Setup(us => us.GetUserByIdAsync(id)).Returns(Task.FromResult(userDTO));
            var userController = new UserController(_userService.Object, elasticClient.Object);

            var result = await userController.GetUserById(id);

            Assert.IsType<OkObjectResult>(result as OkObjectResult);
        }
        [Fact]
        public async Task UserController_GetUserById_NotFound()
        {
            var id = new int();
            UserDTO userDTO = null;
            _userService.Setup(us => us.GetUserByIdAsync(id)).Returns(Task.FromResult(userDTO));
            var userController = new UserController(_userService.Object, elasticClient.Object);

            var result = await userController.GetUserById(id);

            Assert.IsType<NotFoundResult>(result as NotFoundResult);
        }

        [Fact]
        public async Task UserController_AddUser_ReturnCreatedAtRoute()
        {
            var userFormDTO = new Mock<UserFormDTO>();
            var userDTO = new Mock<UserDTO>();
            _userService.Setup(us => us.CreateUserAsync(userFormDTO.Object)).Returns(Task.FromResult(userDTO.Object));
            var userController = new UserController(_userService.Object, elasticClient.Object);

            var result = await userController.CreateUser(userFormDTO.Object);

            Assert.IsType<CreatedAtRouteResult>(result as CreatedAtRouteResult);
        }
        [Fact]
        public async Task UserController_AddUser_BadRequest()
        {
            var userFormDTO = new Mock<UserFormDTO>();
            UserDTO userDTO = null;
            _userService.Setup(us => us.CreateUserAsync(userFormDTO.Object)).Returns(Task.FromResult(userDTO));
            var userController = new UserController(_userService.Object, elasticClient.Object);

            var result = await userController.CreateUser(userFormDTO.Object);

            Assert.IsType<BadRequestResult>(result as BadRequestResult);
        }

        [Fact]

        public async Task UserContoller_UpdateUser_NoContent()
        {
            var userFormDTO = new Mock<UserFormDTO>();
            var id = new int();
            var resultUpdate = new UserDTO();
            _userService.Setup(us => us.UpdateUserAsync(id, userFormDTO.Object)).Returns(Task.FromResult(resultUpdate));
            var userController = new UserController(_userService.Object, elasticClient.Object);

            var result = await userController.UpdateUser(id, userFormDTO.Object);

            Assert.IsType<NoContentResult>(result as NoContentResult);
        }

        [Fact]
        public async Task UserContoller_UpdateUser_NotFound()
        {
            var userFormDTO = new Mock<UserFormDTO>();
            var id = new int();
            var resultUpdate = new UserDTO();
            _userService.Setup(us => us.UpdateUserAsync(id, userFormDTO.Object)).Returns(Task.FromResult(resultUpdate));
            var userController = new UserController(_userService.Object, elasticClient.Object);

            var result = await userController.UpdateUser(id, userFormDTO.Object);

            Assert.IsType<NotFoundResult>(result as NotFoundResult);
        }
        [Fact]
        public async Task UserContoller_DeleteUser_NoContent()
        {
            var id = new int();
            int? resultUpdate = 0;
            _userService.Setup(us => us.DeleteUserAsync(id)).Returns(Task.FromResult(resultUpdate));
            var userController = new UserController(_userService.Object, elasticClient.Object);

            var result = await userController.DeleteUser(id);

            Assert.IsType<NoContentResult>(result as NoContentResult);
        }

        [Fact]
        public async Task UserContoller_DeleteUser_NotFound()
        {
            var id = new int();
            int? resultUpdate = 0;
            _userService.Setup(us => us.DeleteUserAsync(id)).Returns(Task.FromResult(resultUpdate));
            var userController = new UserController(_userService.Object, elasticClient.Object);

            var result = await userController.DeleteUser(id);

            Assert.IsType<NotFoundResult>(result as NotFoundResult);
        }



    }
}
