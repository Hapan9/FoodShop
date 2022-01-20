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
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly IUserService _userService;

        public UserServiceTests()
        {
            var fakeData = new FakeData();
            _userRepositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();

            _userRepositoryMock.Setup(r => r.Get(It.IsAny<Guid>()))
                .ReturnsAsync(fakeData.Users.First());

            _userService = new UserService(_userRepositoryMock.Object, mapperMock.Object);
        }

        [Fact]
        public async Task Get_RepositoryInvokes()
        {
            //Act
            await _userService.Get(It.IsAny<Guid>());

            //Assert
            _userRepositoryMock.Verify(r => r.Get(It.IsAny<Guid>()));
        }

        [Fact]
        public async Task GetAll_RepositoryInvokes()
        {
            //Act
            await _userService.GetAll();

            //Assert
            _userRepositoryMock.Verify(r => r.GetAll());
        }

        [Fact]
        public async Task Create_RepositoryInvokes()
        {
            //Act
            await _userService.Create(It.IsAny<UserDto>());

            //Assert
            _userRepositoryMock.Verify(r => r.Create(It.IsAny<User>()));
            _userRepositoryMock.Verify(r => r.Save());
        }

        [Fact]
        public async Task Update_RepositoryInvokes()
        {
            //Act
            await _userService.Update(It.IsAny<Guid>(), It.IsAny<UserDto>());

            //Assert
            _userRepositoryMock.Verify(r => r.Update(It.IsAny<User>()));
            _userRepositoryMock.Verify(r => r.Get(It.IsAny<Guid>()));
            _userRepositoryMock.Verify(r => r.Save());
        }

        [Fact]
        public async Task Delete_RepositoryInvokes()
        {
            //Act
            await _userService.Delete(It.IsAny<Guid>());

            //Assert
            _userRepositoryMock.Verify(r => r.Delete(It.IsAny<Guid>()));
            _userRepositoryMock.Verify(r => r.Get(It.IsAny<Guid>()));
            _userRepositoryMock.Verify(r => r.Save());
        }
    }
}