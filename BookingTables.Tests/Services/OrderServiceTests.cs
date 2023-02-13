using AutoMapper;
using Core.DTOs;
using Core.IServices;
using Core.Services;
using Moq;
using Xunit;
using Infrastructure.TestExtensions;
using Microsoft.AspNetCore.Http;
using Models.Models;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Tests.Services
{
    public class OrderServiceTests
    {
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
        private readonly Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();
        private readonly Mock<IHttpContextAccessor> _http = new Mock<IHttpContextAccessor>();
        private readonly Mock<IPublishEndpoint> _publishEndpoint = new Mock<IPublishEndpoint>();
        private readonly Mock<ILogger<OrderService>> _logger = new Mock<ILogger<OrderService>>();

        private readonly List<int> _tableDTOs = new List<int>
        {
            0,
            1,
            2
        };
        private readonly List<Table> _tables = new();
        [Fact]
        public async Task OrderService_CreateOrder_Return()
        {
            var orderForCreation = GetOrderFormDTO();
            var order = GetTestOrder();
            var orderDTO = new OrderDTO();


            _mapper.Setup(map => map.Map<Order>(orderForCreation)).Returns(order);
            _mapper.Setup(map => map.Map<OrderDTO>(order)).Returns(orderDTO);

            var quary = order.Table.AsAsyncQueryable();

            _unitOfWork.Setup(rep => rep.TableRepository.GetTablesByIdsAsync(orderForCreation.TablesId, true)).Returns(Task.FromResult(order.Table));
            _unitOfWork.Setup(rep => rep.OrderRepository.Create(order));
            var orderService = new OrderService(_mapper.Object, _unitOfWork.Object, _http.Object, _publishEndpoint.Object,_logger.Object);

            var createdOrder = await orderService.CreateOrderAsync(orderForCreation);

            Assert.NotNull(createdOrder);

        }
        [Fact]
        public async Task OrderService_CreateOrder_TableNullReturn()
        {
            var orderForCreation = GetOrderFormDTO();
            var order = GetTestOrder();
            var orderDTO = new OrderDTO();
            order.Table = new List<Table>();

            _mapper.Setup(map => map.Map<Order>(orderForCreation)).Returns(order);
            _mapper.Setup(map => map.Map<OrderDTO>(order)).Returns(orderDTO);


            _unitOfWork.Setup(rep => rep.TableRepository.GetTablesByIdsAsync(orderForCreation.TablesId, true)).Returns(Task.FromResult(order.Table));

            var orderService = new OrderService(_mapper.Object, _unitOfWork.Object, _http.Object, _publishEndpoint.Object, _logger.Object);

            var createdOrder = await orderService.CreateOrderAsync(orderForCreation);

            Assert.Null(createdOrder);
        }
        [Fact]
        public async Task OrderService_CreateOrder_CountOfSeatsNullReturn()
        {
            var orderForCreation = GetOrderFormDTO();
            var order = GetTestOrder();
            var orderDTO = new OrderDTO();
            orderForCreation.CountOfPeople = 100;

            _mapper.Setup(map => map.Map<Order>(orderForCreation)).Returns(order);
            _mapper.Setup(map => map.Map<OrderDTO>(order)).Returns(orderDTO);

            _unitOfWork.Setup(rep => rep.TableRepository.GetTablesByIdsAsync(orderForCreation.TablesId, true)).Returns(Task.FromResult(order.Table));

            var orderService = new OrderService(_mapper.Object, _unitOfWork.Object, _http.Object, _publishEndpoint.Object, _logger.Object);

            var createdOrder = await orderService.CreateOrderAsync(orderForCreation);

            Assert.Null(createdOrder);
        }
        [Fact]

        public async Task OrderService_DeleteOrder_NotNull()
        {
            var id = new int();
            var order = GetTestOrder();
            _unitOfWork.Setup(or => or.OrderRepository.GetOrderAsync(It.IsAny<int>())).Returns(Task.FromResult(order));

            var orderService = new OrderService(_mapper.Object, _unitOfWork.Object, _http.Object, _publishEndpoint.Object, _logger.Object);
            var result = await orderService.DeleteOrderAsync(id);

            Assert.NotNull(result);
        }
        [Fact]
        public async Task OrderService_DeleteOrder_Null()
        {
            var id = new int();
            Order order = null;
            _unitOfWork.Setup(or => or.OrderRepository.GetOrderAsync(It.IsAny<int>())).Returns(Task.FromResult(order));

            var orderService = new OrderService(_mapper.Object, _unitOfWork.Object, _http.Object, _publishEndpoint.Object, _logger.Object);
            var result = await orderService.DeleteOrderAsync(id);

            Assert.Null(result);
        }
        [Fact]
        public async Task OrderService_GetOrder()
        {
            var order = GetTestOrder();
            var orderDTO = new OrderDTO();
            var id = new int();
            _unitOfWork.Setup(or => or.OrderRepository.GetOrderAsync(It.IsAny<int>())).Returns(Task.FromResult(order));
            _mapper.Setup(map => map.Map<OrderDTO>(order)).Returns(orderDTO);

            var orderService = new OrderService(_mapper.Object, _unitOfWork.Object, _http.Object, _publishEndpoint.Object, _logger.Object);
            var result = await orderService.GetOrderAsync(id);

            Assert.IsType<OrderDTO>(result);
        }
        [Fact]
        public async Task OrderService_GetOrder_NullResult()
        {
            Order order = null;
            var orderDTO = new OrderDTO();
            var id = new int();
            _unitOfWork.Setup(or => or.OrderRepository.GetOrderAsync(It.IsAny<int>())).Returns(Task.FromResult(order));
            _mapper.Setup(map => map.Map<OrderDTO>(order)).Returns(orderDTO);

            var orderService = new OrderService(_mapper.Object, _unitOfWork.Object, _http.Object, _publishEndpoint.Object, _logger.Object);
            var result = await orderService.GetOrderAsync(id);

            Assert.Null(order);
        }
        [Fact]
        public async Task OrderService_UpdateOrder_Return()
        {
            var orderForCreation = GetOrderFormDTO();
            var order = GetTestOrder();
            var orderDTO = new OrderDTO();
            var id = new int();

            _mapper.Setup(map => map.Map<Order>(orderForCreation)).Returns(order);
            _mapper.Setup(map => map.Map<OrderDTO>(order)).Returns(orderDTO);

            _unitOfWork.Setup(rep => rep.TableRepository.GetTablesByIdsAsync(orderForCreation.TablesId, true)).Returns(Task.FromResult(order.Table));
            _unitOfWork.Setup(rep => rep.OrderRepository.GetOrderAsync(It.IsAny<int>())).Returns(Task.FromResult(order));

            var orderService = new OrderService(_mapper.Object, _unitOfWork.Object, _http.Object, _publishEndpoint.Object, _logger.Object);

            var createdOrder = await orderService.UpdateOrderAsync(id, orderForCreation);
            Assert.NotNull(createdOrder);

        }
        [Fact]
        public async Task OrderService_UpdateOrder_FalseTables()
        {
            var orderForCreation = GetOrderFormDTO();
            var order = GetTestOrder();
            var orderDTO = new OrderDTO();
            var id = new int();
            order.Table = new List<Table>();

            _mapper.Setup(map => map.Map<Order>(orderForCreation)).Returns(order);
            _mapper.Setup(map => map.Map<OrderDTO>(order)).Returns(orderDTO);

            var quary = order.Table.AsAsyncQueryable();

            _unitOfWork.Setup(rep => rep.TableRepository.GetTablesByIdsAsync(orderForCreation.TablesId, true)).Returns(Task.FromResult((order.Table)));
            _unitOfWork.Setup(rep => rep.OrderRepository.GetOrderAsync(It.IsAny<int>())).Returns(Task.FromResult(order));

            var orderService = new OrderService(_mapper.Object, _unitOfWork.Object, _http.Object, _publishEndpoint.Object, _logger.Object);

            var updatedOrder = await orderService.UpdateOrderAsync(id, orderForCreation);

            Assert.Null(updatedOrder);

        }
        [Fact]
        public async Task OrderService_UpdateOrder_FalseCountOfSeats()
        {
            var orderForCreation = GetOrderFormDTO();
            var order = GetTestOrder();
            var orderDTO = new OrderDTO();
            var id = new int();
            orderForCreation.CountOfPeople = 100;

            _mapper.Setup(map => map.Map<Order>(orderForCreation)).Returns(order);
            _mapper.Setup(map => map.Map<OrderDTO>(order)).Returns(orderDTO);

            _unitOfWork.Setup(rep => rep.TableRepository.GetTablesByIdsAsync(orderForCreation.TablesId, true)).Returns(Task.FromResult(order.Table));
            _unitOfWork.Setup(rep => rep.OrderRepository.GetOrderAsync(It.IsAny<int>())).Returns(Task.FromResult(order));

            var orderService = new OrderService(_mapper.Object, _unitOfWork.Object, _http.Object, _publishEndpoint.Object, _logger.Object);

            var updatedOrder = await orderService.UpdateOrderAsync(id, orderForCreation);

            Assert.Null(updatedOrder);

        }
        public async Task OrderService_GetOrders()
        {
            var order = GetTestOrder();
            var orderList = new List<Order>() { order };

        }

        public Order GetTestOrder()
        {
            var order = new Order()
            {
                CountOfPeople = 6,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                StartOfReservation = DateTime.Now,
                EndOfReservation = DateTime.Now,


            };
            var tables = new List<Table>();
            var table = new Table()
            {
                CountOfSeats = 3,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Number = 3,
            };
            var table1 = new Table()
            {
                CountOfSeats = 4,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Number = 3,
            };
            var table2 = new Table()
            {
                CountOfSeats = 5,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Number = 3,
            };
            tables.Add(table);
            tables.Add(table1);
            tables.Add(table2);
            order.Table = tables;
            return order;
        }
        public OrderFormDTO GetOrderFormDTO()
        {
            var orderForCreation = new OrderFormDTO()
            {
                CountOfPeople = 6,
                StartOfReservation = DateTime.Now,
                EndOfReservation = DateTime.Now,
                TablesId = _tableDTOs
            };
            return orderForCreation;
        }
    }
}
