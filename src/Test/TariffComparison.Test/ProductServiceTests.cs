using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using TariffComparison.WebApi.Dtos;
using TariffComparison.WebApi.Infrastructure.Interfaces;
using TariffComparison.WebApi.Infrastructure.Models.Product;
using TariffComparison.WebApi.Infrastructure.Services;
using Xunit;

namespace TariffComparison.Test
{
    public class ProductServiceTests
    {
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private readonly HttpClient _httpClient;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            _httpClient = new HttpClient(_mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://localhost/")
            };
            _productService = new ProductService(_httpClient);
        }

        [Fact]
        public async Task GetProducts_ShouldReturnList()
        {
            // Arrange
            List<BaseProduct> expectedProducts = GetProductSamples();

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = JsonContent.Create(expectedProducts)
                })
                .Verifiable();

            // Act
            var actualProducts = await _productService.GetProducts();

            // Assert
            Assert.NotNull(actualProducts);
            Assert.Equal(expectedProducts.Count, actualProducts.Count);
            Assert.Equal(expectedProducts[0].Name, actualProducts[0].Name);
        }

        [Fact]
        public async Task GetAnnualCosts_WithValidConsumption_ShouldReturnOrderedList()
        {
            // Arrange
            double consumption = 3500;
            var products = GetProductSamples();

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = JsonContent.Create(products)
                })
                .Verifiable();

            // Act
            var actualCosts = await _productService.GetAnnualCosts(consumption);

            // Assert
            Assert.NotNull(actualCosts);
            Assert.Equal(2, actualCosts.Count);
            Assert.True(actualCosts[0].AnnualCosts == 800);
            Assert.True(actualCosts[1].AnnualCosts == 830);
        }

        [Fact]
        public async Task GetAnnualCosts_WithNoProducts_ShouldReturnEmptyList()
        {
            // Arrange
            double consumption = 1000;

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = JsonContent.Create(new List<BaseProduct>())
                })
                .Verifiable();

            // Act
            var actualCosts = await _productService.GetAnnualCosts(consumption);

            // Assert
            Assert.NotNull(actualCosts);
            Assert.Empty(actualCosts);
        }

        private static List<BaseProduct> GetProductSamples()
        {
            return new List<BaseProduct>
            {
                new BasicElectricityTariffProduct { Name = "Product A", Type = ProductType.BasicElectricityTariff, BaseCost = 5, AdditionalKwhCost = 22 },
                new PackagedTariffProduct { Name = "Product B", Type = ProductType.PackagedTariff, IncludedKwh = 4000, BaseCost = 800, AdditionalKwhCost = 30 }
            };
        }
    }
}
