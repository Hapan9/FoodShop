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
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly IProductService _productService;

        public ProductServiceTests()
        {
            var fakeData = new FakeData();
            _productRepositoryMock = new Mock<IProductRepository>();
            var mapperMock = new Mock<IMapper>();

            _productRepositoryMock.Setup(r => r.Get(It.IsAny<Guid>()))
                .ReturnsAsync(fakeData.Products.First());

            var scoreServiceMock = new Mock<IProductScoreService>();

            _productService =
                new ProductService(_productRepositoryMock.Object, mapperMock.Object, scoreServiceMock.Object);
        }

        [Fact]
        public async Task Get_RepositoryInvokes()
        {
            //Act
            await _productService.Get(It.IsAny<Guid>());

            //Assert
            _productRepositoryMock.Verify(r => r.Get(It.IsAny<Guid>()));
        }

        [Fact]
        public async Task GetAll_RepositoryInvokes()
        {
            //Act
            await _productService.GetAll();

            //Assert
            _productRepositoryMock.Verify(r => r.GetAll());
        }

        [Fact]
        public async Task Create_RepositoryInvokes()
        {
            //Act
            await _productService.Create(It.IsAny<ProductDto>());

            //Assert
            _productRepositoryMock.Verify(r => r.Create(It.IsAny<Product>()));
            _productRepositoryMock.Verify(r => r.Save());
        }

        [Fact]
        public async Task Update_RepositoryInvokes()
        {
            //Act
            await _productService.Update(It.IsAny<Guid>(), It.IsAny<ProductDto>());

            //Assert
            _productRepositoryMock.Verify(r => r.Update(It.IsAny<Product>()));
            _productRepositoryMock.Verify(r => r.Get(It.IsAny<Guid>()));
            _productRepositoryMock.Verify(r => r.Save());
        }

        [Fact]
        public async Task Delete_RepositoryInvokes()
        {
            //Act
            await _productService.Delete(It.IsAny<Guid>());

            //Assert
            _productRepositoryMock.Verify(r => r.Delete(It.IsAny<Guid>()));
            _productRepositoryMock.Verify(r => r.Get(It.IsAny<Guid>()));
            _productRepositoryMock.Verify(r => r.Save());
        }
    }
}