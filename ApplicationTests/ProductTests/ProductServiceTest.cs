using Application.Services.Implementation;
using Domain.Exceptions;
using Dtos.InfrastructureDtos.HttpClientDtos.BalanceManagementClientDtos.ProductDtos;
using Infrastructure.HttpClients.Abstract;
using Moq;

namespace ApplicationTests.ProductTests
{
    public class ProductServiceTest
    {
        private readonly Mock<IBalanceManagementClient> balanceManagementMock;
        public ProductServiceTest()
        {
            balanceManagementMock = new Mock<IBalanceManagementClient>();
        }

        [Fact]
        public async Task GetProducts_ShouldThrowNotFoundException_WhenNoProducts()
        {
            //Arrange
            balanceManagementMock.Setup(x => x.GetProductsAsync())
                  .ReturnsAsync(new GetProductsClientResponseDto(true, new List<ProductDto>()));
                        
            var service = new ProductService(balanceManagementMock.Object);

            //Act && Assert
            await Assert.ThrowsAsync<NotFoundException>(service.GetProducts);
        }

        [Fact]
        public async Task GetProducts_ShouldReturn_Data()
        {
            //Arrange
            var productList = new List<ProductDto>()
            {
                new ProductDto("ID-101","Moon Phone","Latest Feature Phone",100,"USD","Electronics",25)
            };

            balanceManagementMock.Setup(x => x.GetProductsAsync())
                  .ReturnsAsync(new GetProductsClientResponseDto(true, productList));

            //Act
            var service = new ProductService(balanceManagementMock.Object);
            
            var result = await service.GetProducts();
            //Assert
            Assert.Equal(productList, result);
        }

        [Fact]
        public async Task GetProducts_ShouldThrowNotFoundException_WhenServiceReturnsNull()
        {
            // Arrange
            balanceManagementMock.Setup(x => x.GetProductsAsync())
                      .ReturnsAsync((GetProductsClientResponseDto?)null);

            // Act
            var service = new ProductService(balanceManagementMock.Object);

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(service.GetProducts);
        }

    }
}
