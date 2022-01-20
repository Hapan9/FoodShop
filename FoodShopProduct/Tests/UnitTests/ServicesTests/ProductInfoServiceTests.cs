using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Dto;
using BLL.Implementation;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using Moq;
using Xunit;

namespace Tests.UnitTests.ServicesTests
{
    public class ProductInfoServiceTests
    {
        private readonly Mock<IProductInfoRepository> _productInfoRepositoryMock;
        private readonly IProductInfoService _productInfoService;

        public ProductInfoServiceTests()
        {
            var fakeData = new FakeData();
            _productInfoRepositoryMock = new Mock<IProductInfoRepository>();
            var mapperMock = new Mock<IMapper>();

            _productInfoRepositoryMock.Setup(r => r.Get(It.IsAny<Guid>()))
                .ReturnsAsync(fakeData.ProductsInfo.First());

            _productInfoService = new ProductInfoService(_productInfoRepositoryMock.Object, mapperMock.Object);
        }

        [Fact]
        public async Task Get_RepositoryInvokes()
        {
            //Act
            await _productInfoService.Get(It.IsAny<Guid>());

            //Assert
            _productInfoRepositoryMock.Verify(r => r.Get(It.IsAny<Guid>()));
        }

        [Fact]
        public async Task GetAll_RepositoryInvokes()
        {
            //Act
            await _productInfoService.GetAll();

            //Assert
            _productInfoRepositoryMock.Verify(r => r.GetAll());
        }

        [Fact]
        public async Task Create_RepositoryInvokes()
        {
            //Act
            await _productInfoService.Create(It.IsAny<ProductInfoDto>());

            //Assert
            _productInfoRepositoryMock.Verify(r => r.Create(It.IsAny<ProductInfo>()));
            _productInfoRepositoryMock.Verify(r => r.Save());
        }

        [Fact]
        public async Task Update_RepositoryInvokes()
        {
            //Act
            await _productInfoService.Update(It.IsAny<Guid>(), It.IsAny<ProductInfoDto>());

            //Assert
            _productInfoRepositoryMock.Verify(r => r.Update(It.IsAny<ProductInfo>()));
            _productInfoRepositoryMock.Verify(r => r.Get(It.IsAny<Guid>()));
            _productInfoRepositoryMock.Verify(r => r.Save());
        }

        [Fact]
        public async Task Delete_RepositoryInvokes()
        {
            //Act
            await _productInfoService.Delete(It.IsAny<Guid>());

            //Assert
            _productInfoRepositoryMock.Verify(r => r.Delete(It.IsAny<Guid>()));
            _productInfoRepositoryMock.Verify(r => r.Get(It.IsAny<Guid>()));
            _productInfoRepositoryMock.Verify(r => r.Save());
        }
    }
}