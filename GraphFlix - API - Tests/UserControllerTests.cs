namespace GraphFlix___API___Tests
{
    public class UserControllerTests
    {
        [Fact]
        public async Task Get_ShouldReturnUsers()
        {
            // Arrange
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IUserRepository>()
                    .Setup(ur => ur.GetAll())
                    .ReturnsAsync(await SampleData());

                var controller = mock.Create<UserController>();

                // Act
                var result = await controller.Get();
                var listOfUsers = (result.Result as ObjectResult).Value;
                IEnumerable<UserDto> sampleDtos = await SampleData();

                string actualUsersJson = JsonSerializer.Serialize(listOfUsers);
                string expectedUsersJson = JsonSerializer.Serialize(sampleDtos);

                // Assert
                Assert.True(result != null); // Null check
                Assert.Equal(expectedUsersJson, actualUsersJson);
            }
        }

        [Fact]
        public async Task GetById_ShouldReturnUser()
        {
            // Arrange
            using (var mock = AutoMock.GetLoose())
            {
                var sampleData = await SampleData();
                UserDto sampleUser = sampleData.ElementAt(1);
                mock.Mock<IUserRepository>()
                    .Setup(ur => ur.GetById(sampleUser.Id))
                    .ReturnsAsync(sampleUser);

                var controller = mock.Create<UserController>();

                // Act
                var result = await controller.Get(sampleUser.Id);
                var returnedUser = (result.Result as ObjectResult).Value;

                string actualUserJson = JsonSerializer.Serialize(returnedUser);
                string expectedUserJson = JsonSerializer.Serialize(sampleUser);

                // Assert
                Assert.True(result != null); // Null check
                Assert.Equal(expectedUserJson, actualUserJson);
            }
        }

        private async Task<IEnumerable<UserDto>> SampleData()
        {
            List<UserDto> samples = new List<UserDto>()
            {
                new UserDto() { Id = 1, Username = "JohnDoe" },
                new UserDto() { Id = 2, Username = "JaneDoe" },
                new UserDto() { Id = 3, Username = "WayneDoe"},
                new UserDto() { Id = 4, Username = "Blaine Doe" },
                new UserDto() { Id = 5, Username = "Germaine Doe" }
            };
            return samples;
        }
    }
}
