using BookingTables.Shared.EventModels;
using Core.IServices;
using MassTransit;

namespace BookingTables.Core.Services
{
    public class UserConsumer : IConsumer<UserInfo>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserConsumer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Consume(ConsumeContext<UserInfo> context)
        {
            var userId = context.Message.Id;
            var userName = context.Message.UserName;

            var orders = await _unitOfWork.OrderRepository.FindAllUserOrdersAsync(userId);
            orders.ForEach(order=>order.UserName = userName);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
