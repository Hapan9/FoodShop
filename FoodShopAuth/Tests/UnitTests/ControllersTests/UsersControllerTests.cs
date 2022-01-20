using System;
using System.Threading.Tasks;
using BLL.Dto;
using BLL.Interfaces;
using Moq;
using UI.Controllers;
using Xunit;

namespace Tests.UnitTests.ControllersTests
{
    public class UsersControllerTests
    {
        private readonly UsersController _usersController;
        private readonly Mock<IUserService> _userServiceMock;

        public UsersControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _usersController = new UsersController(_userServiceMock.Object);
        }

        [Fact]
        public async Task Get_ServiceInvoke()
        {
            //Act
            await _usersController.Get(It.IsAny<Guid>());

            //Assert
            _userServiceMock.Verify(s => s.Get(It.IsAny<Guid>()));
        }

        [Fact]
        public async Task GetAll_ServiceInvoke()
        {
            //Act
            await _usersController.GetAll();

            //Assert
            _userServiceMock.Verify(s => s.GetAll());
        }

        [Fact]
        public async Task Create_ServiceInvoke()
        {
            //Act
            await _usersController.Create(It.IsAny<UserDto>());

            //Assert
            _userServiceMock.Verify(s => s.Create(It.IsAny<UserDto>()));
        }

        [Fact]
        public async Task Update_ServiceInvoke()
        {
            //Act
            await _usersController.Update(It.IsAny<Guid>(), It.IsAny<UserDto>());

            //Assert
            _userServiceMock.Verify(s => s.Update(It.IsAny<Guid>(), It.IsAny<UserDto>()));
        }

        [Fact]
        public async Task Delete_ServiceInvoke()
        {
            //Act
            await _usersController.Delete(It.IsAny<Guid>());

            //Assert
            _userServiceMock.Verify(s => s.Delete(It.IsAny<Guid>()));
        }
    }
}