using Application.Services.Implementation;
using Domain.Exceptions;
using Dtos.InfrastructureDtos.HttpClientDtos.BalanceManagementClientDtos.OrderDtos;
using Infrastructure.DbContexts.Abstract;
using Infrastructure.DbContexts.Implementation;
using Infrastructure.HttpClients.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace ApplicationTests.OrderTests
{
    public class OrderServiceTests
    {
        private readonly Mock<IBalanceManagementClient> balanceManagementMock;
        private readonly Mock<IDbContext<PostgresDbContext>> dbContextMock;
        private readonly PostgresDbContext inMemoryDbContext;

        public OrderServiceTests()
        {
            balanceManagementMock = new Mock<IBalanceManagementClient>();

            var options = new DbContextOptionsBuilder<PostgresDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var configMock = new Mock<IConfiguration>();
            inMemoryDbContext = new PostgresDbContext(options, configMock.Object);

            dbContextMock = new Mock<IDbContext<PostgresDbContext>>();
            dbContextMock.Setup(x => x.Instance).Returns(inMemoryDbContext);
        }

        [Fact]
        public async Task CompleteOrder_ShouldReturnResponse_AndLogToDb()
        {
            // Arrange
            var request = new CompleteOrderRequestDto("order-123");
            var response = new OrderResponseDto(true, "Order completed", new(null, new UpdatedBalanceDto(Guid.NewGuid(), 100, 100, 0, "USD", DateTime.Now)));

            balanceManagementMock
                .Setup(x => x.CompleteAsync(It.IsAny<CompleteOrderRequestDto>()))
                .ReturnsAsync(response);

            var orderService = new OrderService(balanceManagementMock.Object, dbContextMock.Object);

            // Act
            var result = await orderService.CompleteOrder(request);

            // Assert
            Assert.True(result.success);
            Assert.Equal("Order completed", result.message);

            var logs = await inMemoryDbContext.OrderLogs.ToListAsync();
            Assert.Single(logs);
            Assert.Equal("Order completed", logs.First().Message);
        }

        [Fact]
        public async Task CompleteOrder_ShouldThrow_BadRequest_When_OrderId_Is_NullOrEmpty()
        {
            var request = new CompleteOrderRequestDto("");

            var orderService = new OrderService(balanceManagementMock.Object, dbContextMock.Object);

            //Act && Assert
            var exception = await Assert.ThrowsAsync<BadRequestException>(() =>
            orderService.CompleteOrder(request));
        }

        [Fact]
        public async Task CreateOrder_Should_Throw_BadRequest_When_Amount_Lower_Then_Zero()
        {
            //Arrange
            var request = new PreOrderRequestDto("", -1);

            var orderService = new OrderService(balanceManagementMock.Object, dbContextMock.Object);

            //Act && Assert
            var exception = await Assert.ThrowsAsync<BadRequestException>(() =>
            orderService.CreateOrder(request));
        }


        [Fact]
        public async Task CreateOrder_Should_Throw_BadRequest_When_Amount_Equal_To_Zero()
        {
            //Arrange
            var request = new PreOrderRequestDto("", 0);

            var orderService = new OrderService(balanceManagementMock.Object, dbContextMock.Object);

            //Act && Assert
            var exception = await Assert.ThrowsAsync<BadRequestException>(() =>
            orderService.CreateOrder(request));
        }

        [Fact]
        public async Task CreateOrder_ShouldReturnResponse_AndLogToDb()
        {
            //Arrange
            var request = new PreOrderRequestDto("", 1);
            var response = new OrderResponseDto(true, "Order created", new(null, new UpdatedBalanceDto(Guid.NewGuid(), 100, 100, 0, "USD", DateTime.Now)));

            balanceManagementMock
                .Setup(x => x.PreOrderAsync(It.IsAny<PreOrderRequestDto>()))
                .ReturnsAsync(response);

            var orderService = new OrderService(balanceManagementMock.Object, dbContextMock.Object);

            // Act
            var result = await orderService.CreateOrder(request);

            // Assert
            Assert.True(result.success);
            Assert.Equal("Order created", result.message);

            var logs = await inMemoryDbContext.OrderLogs.ToListAsync();
            Assert.Single(logs);
            Assert.Equal("Order created", logs.First().Message);
        }
    }
}
