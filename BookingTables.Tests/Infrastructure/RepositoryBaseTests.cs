using Infrastructure.IRepositories;
using Models.Models;
using Moq;
using BookingTables.Shared.RequestModels;
using Xunit;

namespace Infrastructure.Tests
{
    public class RepositoryBaseTests
    {
        private readonly Mock<RequestFeatures> _requestFeaturesMock = new Mock<RequestFeatures>();
        [Fact]
        public void RepositoryBase_FindAll()
        {

            var table = GetTestProduct();
            var userList = new List<Table>
            {
                table
            };

            var mockRepo = new Mock<IRepositoryBase< Table , RequestFeatures>>();
            mockRepo.Setup(ex => ex.FindAllAsync(false, _requestFeaturesMock.Object)).Returns(Task.FromResult(userList));
            var users = (mockRepo.Object.FindAllAsync(false, _requestFeaturesMock.Object));

            Assert.True(users.Result.Count >= 1);
        }

        [Fact]
        public void RepositoryBase_Add()
        {
            var table = GetTestProduct();
            var mockRepo = new Mock<IRepositoryBase<Table, RequestFeatures>>();
            mockRepo.Setup(us => us.Create(table)).Verifiable();

            mockRepo.Object.Create(table);

            mockRepo.Verify();
        }
        [Fact]
        public void RepositoryBase_Update()
        {
            var table = GetTestProduct();
            var mockRepo = new Mock<IRepositoryBase<Table, RequestFeatures>>();
            mockRepo.Setup(us => us.Update(table)).Verifiable();

            mockRepo.Object.Update(table);

            mockRepo.Verify();
        }
        [Fact]
        public void RepositoryBase_Delete()
        {
            var table = GetTestProduct();
            var mockRepo = new Mock<IRepositoryBase<Table, RequestFeatures>>();
            mockRepo.Setup(us => us.Delete(table)).Verifiable();

            mockRepo.Object.Delete(table);

            mockRepo.Verify();
        }
        public Table GetTestProduct()
        {

            Table user = new Table()
            {
                Id = 1,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                Number = 1,
                CountOfSeats = 3,

            };
            return user;
        }
    }
}
