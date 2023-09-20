using Autofac.Extras.Moq;
using GraphFlix.DTOs;
using GraphFlix.Managers;
using GraphFlix.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GraphFlix___API___Tests
{
    public class UserManagerTests
    {
        [Fact]
        public async Task GetUsers_ShouldReturnUsers()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                mock.Mock<IUserRepository>()
                    .Setup(repo => repo.GetAll())
                    .ReturnsAsync(SampleData());

                var manager = mock.Create<UserManager>();

                // Act
                var result = await manager.GetUsers();
                string actualJson = JsonSerializer.Serialize(result);
                string expectedJson = JsonSerializer.Serialize(SampleData());

                // Assert
                Assert.True(result != null);
                Assert.Equal(expectedJson, actualJson);
            }
        }

        [Fact]
        public async Task GetUser_ShouldReturnUser()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                var sampleUser = SampleData().ElementAt(1);
                mock.Mock<IUserRepository>()
                    .Setup(repo => repo.GetById(sampleUser.Id))
                    .ReturnsAsync(sampleUser);

                var manager = mock.Create<UserManager>();

                // Act
                var result = await manager.GetUser(sampleUser.Id);
                string actualJson = JsonSerializer.Serialize(result);
                string expectedJson = JsonSerializer.Serialize(sampleUser);

                // Assert
                Assert.True(result != null);
                Assert.Equal(expectedJson, actualJson);
            }
        }

        [Fact]
        public async Task UpdateUser_ShouldReturnWithChanges()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                UserDto sampleUser = SampleData().ElementAt(1);
                sampleUser.Username = "Keanu Reeves";

                mock.Mock<IUserRepository>()
                    .Setup(repo => repo.Update(sampleUser))
                    .ReturnsAsync(sampleUser);

                var manager = mock.Create<UserManager>();

                // Act
                var result = await manager.UpdateUser(sampleUser);
                string actualJson = JsonSerializer.Serialize(result);
                string expectedJson = JsonSerializer.Serialize(sampleUser);

                // Assert
                Assert.True(result != null);
                Assert.Equal(expectedJson, actualJson);
            }
        }

        private IEnumerable<UserDto> SampleData()
        {
            List<UserDto> samples = new List<UserDto>()
            {
                new UserDto("John Doe") { Id = "1" },
                new UserDto("Jane Doe") { Id = "2" },
                new UserDto("Wayne Doe") { Id = "3" },
                new UserDto("Blaine Doe") { Id = "4" },
                new UserDto("Germaine Doe") { Id = "5" }
            };
            return samples;
        }
    }
}
