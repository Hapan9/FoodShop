using System;
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
    public class ProductsInfoControllerTests
    {
        private readonly FakeData _fake;

        private readonly BaseTestFixture _fixture;

        public ProductsInfoControllerTests()
        {
            _fixture = new BaseTestFixture();
            _fake = new FakeData();
        }

        [Fact]
        public async Task Get_ShouldReturnListResult()
        {
            // Act
            var response = await _fixture.Client.GetAsync("api/ProductsInfo");
            response.EnsureSuccessStatusCode();
            var models =
                JsonConvert.DeserializeObject<ICollection<ProductInfo>>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.NotEmpty(models);
        }

        [Fact]
        public async Task GetById_ShouldReturnResult()
        {
            //Arrange
            var productInfoId = _fixture.ProductContext.ProductInfos.First().Id;

            //Act
            var response = await _fixture.Client.GetAsync("api/ProductsInfo/" + productInfoId);
            response.EnsureSuccessStatusCode();
            var models =
                JsonConvert.DeserializeObject<ProductInfo>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.NotNull(models);
        }

        [Fact]
        public async Task Post_ShouldCreateNewProductInfo()
        {
            //Arrange
            var productInfoDto = _fake.Fixture.Create<ProductDto>();
            var fakeProductDtoJson = JsonConvert.SerializeObject(productInfoDto);
            var httpContent = new StringContent(fakeProductDtoJson, Encoding.UTF8, "application/json");

            //Act
            var response = await _fixture.Client.PostAsync("api/ProductsInfo", httpContent);
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.Contains(_fixture.ProductContext.ProductInfos, p => p.Name == productInfoDto.Name);
        }

        [Fact]
        public async Task Put_ShouldUpdateExistingProductInfo()
        {
            //Arrange
            var mapper = AutoMapperProfile.InitializeAutoMapper().CreateMapper();
            var productInfo = _fixture.ProductContext.ProductInfos.First();
            productInfo.Name = "Name";
            var productInfoJson = JsonConvert.SerializeObject(mapper.Map<ProductInfoDto>(productInfo));

            var httpContent = new StringContent(productInfoJson, Encoding.UTF8, "application/json");

            //Act
            var response = await _fixture.Client.PutAsync("api/ProductsInfo/" + productInfo.Id, httpContent);
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.Contains(_fixture.ProductContext.ProductInfos, p => p.Name == "Name");
        }

        [Fact]
        public async Task Delete_ShouldDeleteExistingProductInfo()
        {
            //Arrange
            var productInfoId = _fixture.ProductContext.ProductInfos.First().Id;

            //Act
            var response = await _fixture.Client.DeleteAsync("api/ProductsInfo/" + productInfoId);
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.Equal(0, _fixture.ProductContext.ProductInfos.Count(p => p.Id == productInfoId));
        }

        [Fact]
        public async Task GetProducts_ShouldReturnListResult()
        {
            //Arrange
            var productInfoName = _fixture.ProductContext.ProductInfos.First().Name;

            // Act
            var response = await _fixture.Client.GetAsync($"api/ProductsInfo/{productInfoName}/Products");
            response.EnsureSuccessStatusCode();
            var models =
                JsonConvert.DeserializeObject<ICollection<Guid>>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.NotEmpty(models);
        }

        [Fact]
        public async Task GetNames_ShouldReturnListResult()
        {
            // Act
            var response = await _fixture.Client.GetAsync("api/ProductsInfo/Names");
            response.EnsureSuccessStatusCode();
            var models =
                JsonConvert.DeserializeObject<ICollection<string>>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.NotEmpty(models);
        }
    }
}