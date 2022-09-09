using Core.IServices.IRepositories;
using Core.Models;
using FakeItEasy;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Tests
{
    public class RepositoryBaseTests
    {
        [Fact]
        public void RepositoryBase_FindAll()
        {

            var user = GetTestProduct();
            var userList = new List<User>
            {
                user
            }.AsQueryable();

            var mockRepo = new Mock<IRepositoryBase<User>>();
            mockRepo.Setup(ex => ex.FindAll(false)).Returns(userList);
            IQueryable<User> users = mockRepo.Object.FindAll(false);

            Assert.True(users.Count() >= 1);
        }

        [Fact]
        public void RepositoryBase_Add()
        {
            var user = GetTestProduct();
            var mockRepo = new Mock<IRepositoryBase<User>>();
            mockRepo.Setup(us => us.Create(user)).Verifiable();

            mockRepo.Object.Create(user);

            mockRepo.Verify();
        }
        [Fact]
        public void RepositoryBase_Update()
        {
            var user = GetTestProduct();
            var mockRepo = new Mock<IRepositoryBase<User>>();
            mockRepo.Setup(us => us.Update(user)).Verifiable();

            mockRepo.Object.Update(user);

            mockRepo.Verify();
        }
        [Fact]
        public void RepositoryBase_Delete()
        {
            var user = GetTestProduct();
            var mockRepo = new Mock<IRepositoryBase<User>>();
            mockRepo.Setup(us => us.Delete(user)).Verifiable();

            mockRepo.Object.Delete(user);

            mockRepo.Verify();
        }
        public User GetTestProduct()
        {
            Mock<Avatar> avatar = new Mock<Avatar>();
            User user = new User()
            {
                Id = 1,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                Avatar = avatar.Object,
                Email = "test",
                Name = "test",
                PasswordHash = "test",
                Role = "test"

            };
            return user;
        }
    }
}
