using FakeItEasy;
using Infrastructure.IRepositories;
using Models.Models;
using Moq;
using Shared.RequestModels;
using Xunit;

namespace Infrastructure.Tests
{
    public class RepositoryBaseTests
    {
        private readonly Mock<RequestFeatures> _requestFeaturesMock = new Mock<RequestFeatures>();
        [Fact]
        public void RepositoryBase_FindAll()
        {

            var user = GetTestProduct();
            var userList = new List<User>
            {
                user
            };

            var mockRepo = new Mock<IRepositoryBase<User,RequestFeatures>>();
            mockRepo.Setup(ex => ex.FindAllAsync(false,_requestFeaturesMock.Object)).Returns(Task.FromResult(userList));
            var users = (mockRepo.Object.FindAllAsync(false, _requestFeaturesMock.Object));

            Assert.True(users.Result.Count >= 1);
        }

        [Fact]
        public void RepositoryBase_Add()
        {
            var user = GetTestProduct();
            var mockRepo = new Mock<IRepositoryBase<User,RequestFeatures>>();
            mockRepo.Setup(us => us.Create(user)).Verifiable();

            mockRepo.Object.Create(user);

            mockRepo.Verify();
        }
        [Fact]
        public void RepositoryBase_Update()
        {
            var user = GetTestProduct();
            var mockRepo = new Mock<IRepositoryBase<User,RequestFeatures>>();
            mockRepo.Setup(us => us.Update(user)).Verifiable();

            mockRepo.Object.Update(user);

            mockRepo.Verify();
        }
        [Fact]
        public void RepositoryBase_Delete()
        {
            var user = GetTestProduct();
            var mockRepo = new Mock<IRepositoryBase<User,RequestFeatures>>();
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
