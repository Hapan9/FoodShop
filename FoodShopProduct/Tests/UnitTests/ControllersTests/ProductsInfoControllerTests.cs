using System;
using System.Threading.Tasks;
using BLL.Dto;
using BLL.Interfaces;
using Moq;
using UI.Controllers;
using Xunit;

namespace Tests.UnitTests.ControllersTests
{
    public class ProductsInfoControllerTests
    {
        private readonly Mock<IProductInfoService> _productInfoServiceMock;
        private readonly ProductsInfoController _productsInfoController;

        public ProductsInfoControllerTests()
        {
            _productInfoServiceMock = new Mock<IProductInfoService>();
            _productsInfoController = new ProductsInfoController(_productInfoServiceMock.Object);
        }

        [Fact]
        public async Task Get_ServiceInvoke()
        {
            //Act
            await _productsInfoController.Get(It.IsAny<Guid>());

            //Assert
            _productInfoServiceMock.Verify(s => s.Get(It.IsAny<Guid>()));
        }

        [Fact]
        public async Task GetAll_ServiceInvoke()
        {
            //Act
            await _productsInfoController.GetAll();

            //Assert
            _productInfoServiceMock.Verify(s => s.GetAll());
        }

        [Fact]
        public async Task Create_ServiceInvoke()
        {
            //Act
            await _productsInfoController.Create(It.IsAny<ProductInfoDto>());

            //Assert
            _productInfoServiceMock.Verify(s => s.Create(It.IsAny<ProductInfoDto>()));
        }

        [Fact]
        public async Task Update_ServiceInvoke()
        {
            //Act
            await _productsInfoController.Update(It.IsAny<Guid>(), It.IsAny<ProductInfoDto>());

            //Assert
            _productInfoServiceMock.Verify(s => s.Update(It.IsAny<Guid>(), It.IsAny<ProductInfoDto>()));
        }

        [Fact]
        public async Task Delete_ServiceInvoke()
        {
            //Act
            await _productsInfoController.Delete(It.IsAny<Guid>());

            //Assert
            _productInfoServiceMock.Verify(s => s.Delete(It.IsAny<Guid>()));
        }

        [Fact]
        public async Task GetProducts_ServiceInvoke()
        {
            //Act
            await _productsInfoController.GetProducts(It.IsAny<string>());

            //Assert
            _productInfoServiceMock.Verify(s => s.GetAll());
        }

        [Fact]
        public async Task GetNames_ServiceInvoke()
        {
            //Act
            await _productsInfoController.GetNames();

            //Assert
            _productInfoServiceMock.Verify(s => s.GetAll());
        }
    }
}