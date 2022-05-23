using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using BLL;
using BLL.Dto;
using DAL.Models;
using Newtonsoft.Json;
using Tests.IntegrationTests.Util;
using Xunit;

namespace Tests.IntegrationTests
{
    public class ProductsControllerTests
    {
        private readonly FakeData _fake;

        private readonly BaseTestFixture _fixture;

        public ProductsControllerTests()
        {
            _fixture = new BaseTestFixture();
            _fake = new FakeData();
        }

        [Fact]
        public async Task Get_ShouldReturnListResult()
        {
            // Act
            var response = await _fixture.Client.GetAsync("api/Products");
            response.EnsureSuccessStatusCode();
            var models =
                JsonConvert.DeserializeObject<IEnumerable<Product>>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.NotEmpty(models);
        }

        [Fact]
        public async Task GetById_ShouldReturnResult()
        {
            //Arrange
            var productId = _fixture.ProductContext.Products.First().Id;

            //Act
            var response = await _fixture.Client.GetAsync("api/Products/" + productId);
            response.EnsureSuccessStatusCode();
            var models =
                JsonConvert.DeserializeObject<Product>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.NotNull(models);
        }

        [Fact]
        public async Task Post_ShouldCreateNewProduct()
        {
            //Arrange
            var productDto = _fake.Fixture.Create<ProductDto>();
            var fakeProductDtoJson = JsonConvert.SerializeObject(productDto);
            var httpContent = new StringContent(fakeProductDtoJson, Encoding.UTF8, "application/json");

            //Act
            var response = await _fixture.Client.PostAsync("api/Products", httpContent);
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.Contains(_fixture.ProductContext.Products, p => p.Name == productDto.Name);
        }

        [Fact]
        public async Task Put_ShouldUpdateExistingProduct()
        {
            //Arrange
            var mapper = AutoMapperProfile.InitializeAutoMapper().CreateMapper();
            var product = _fixture.ProductContext.Products.First();
            product.Name = "Name";
            var productJson = JsonConvert.SerializeObject(mapper.Map<ProductDto>(product));

            var httpContent = new StringContent(productJson, Encoding.UTF8, "application/json");

            //Act
            var response = await _fixture.Client.PutAsync("api/Products/" + product.Id, httpContent);
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.Contains(_fixture.ProductContext.Products, p => p.Name == "Name");
        }

        [Fact]
        public async Task Delete_ShouldDeleteExistingProduct()
        {
            //Arrange
            var productId = _fixture.ProductContext.Products.First().Id;

            //Act
            var response = await _fixture.Client.DeleteAsync("api/Products/" + productId);
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.Equal(0, _fixture.ProductContext.Products.Count(p => p.Id == productId));
        }
    }
}