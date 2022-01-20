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
    public class UserControllerTests
    {
        private readonly FakeData _fake;

        private readonly BaseTestFixture _fixture;

        public UserControllerTests()
        {
            _fixture = new BaseTestFixture();
            _fake = new FakeData();
        }

        [Fact]
        public async Task Get_ShouldReturnListResult()
        {
            // Act
            var response = await _fixture.Client.GetAsync("api/Users");
            response.EnsureSuccessStatusCode();
            var models =
                JsonConvert.DeserializeObject<ICollection<User>>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.NotEmpty(models);
        }

        [Fact]
        public async Task GetById_ShouldReturnResult()
        {
            //Arrange
            var userId = _fixture.UserContext.Users.First().Id;

            //Act
            var response = await _fixture.Client.GetAsync("api/Users/" + userId);
            response.EnsureSuccessStatusCode();
            var models =
                JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.NotNull(models);
        }

        [Fact]
        public async Task Post_ShouldCreateNewUser()
        {
            //Arrange
            var userDto = _fake.Fixture.Create<UserDto>();
            var fakeUserDtoJson = JsonConvert.SerializeObject(userDto);
            var httpContent = new StringContent(fakeUserDtoJson, Encoding.UTF8, "application/json");

            //Act
            var response = await _fixture.Client.PostAsync("api/Users", httpContent);
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.Contains(_fixture.UserContext.Users, u => u.Name == userDto.Name);
        }

        [Fact]
        public async Task Put_ShouldUpdateExistingUser()
        {
            //Arrange
            var mapper = AutoMapperProfile.InitializeAutoMapper().CreateMapper();
            var user = _fixture.UserContext.Users.First();
            user.Name = "Name";
            var userJson = JsonConvert.SerializeObject(mapper.Map<UserDto>(user));

            var httpContent = new StringContent(userJson, Encoding.UTF8, "application/json");

            //Act
            var response = await _fixture.Client.PutAsync("api/Users/" + user.Id, httpContent);
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.Contains(_fixture.UserContext.Users, u => u.Name == "Name");
        }

        [Fact]
        public async Task Delete_ShouldDeleteExistingUser()
        {
            //Arrange
            var userId = _fixture.UserContext.Users.First().Id;

            //Act
            var response = await _fixture.Client.DeleteAsync("api/Users/" + userId);
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.Equal(0, _fixture.UserContext.Users.Count(u => u.Id == userId));
        }
    }
}