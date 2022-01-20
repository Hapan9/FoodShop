using System;
using System.Threading.Tasks;
using BLL.Dto;
using BLL.Interfaces;
using Moq;
using UI.Controllers;
using Xunit;

namespace Tests.UnitTests.ControllersTests
{
    public class ProductsControllerTests
    {
        private readonly ProductsController _productsController;
        private readonly Mock<IProductService> _productServiceMock;

        public ProductsControllerTests()
        {
            _productServiceMock = new Mock<IProductService>();
            _productsController = new ProductsController(_productServiceMock.Object);
        }

        [Fact]
        public async Task Get_ServiceInvoke()
        {
            //Act
            await _productsController.Get(It.IsAny<Guid>());

            //Assert
            _productServiceMock.Verify(s => s.Get(It.IsAny<Guid>()));
        }

        [Fact]
        public async Task GetAll_ServiceInvoke()
        {
            //Act
            await _productsController.GetAll();

            //Assert
            _productServiceMock.Verify(s => s.GetAll());
        }

        [Fact]
        public async Task Create_ServiceInvoke()
        {
            //Act
            await _productsController.Create(It.IsAny<ProductDto>());

            //Assert
            _productServiceMock.Verify(s => s.Create(It.IsAny<ProductDto>()));
        }

        [Fact]
        public async Task Update_ServiceInvoke()
        {
            //Act
            await _productsController.Update(It.IsAny<Guid>(), It.IsAny<ProductDto>());

            //Assert
            _productServiceMock.Verify(s => s.Update(It.IsAny<Guid>(), It.IsAny<ProductDto>()));
        }

        [Fact]
        public async Task Delete_ServiceInvoke()
        {
            //Act
            await _productsController.Delete(It.IsAny<Guid>());

            //Assert
            _productServiceMock.Verify(s => s.Delete(It.IsAny<Guid>()));
        }
    }
}