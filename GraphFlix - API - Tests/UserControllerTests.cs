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
                mock.Mock<IUserManager>()
                    .Setup(mm => mm.GetUsers())
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
                mock.Mock<IUserManager>()
                    .Setup(mm => mm.GetUser(sampleUser.Id))
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

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task GetById_ShouldThrowNullException(string? emptyId)
        {
            // Arrange
            using (var mock = AutoMock.GetLoose())
            {
                UserDto? UserDto = null;

                mock.Mock<IUserManager>()
                    .Setup(mm => mm.GetUser(emptyId))
                    .ReturnsAsync(UserDto);

                var controller = mock.Create<UserController>();

                // Act
                var result = await controller.Get(emptyId);

                // Assert
                Assert.IsType<StatusCodeResult>(result.Result);
                Assert.Equal(StatusCodes.Status400BadRequest, (result.Result as StatusCodeResult).StatusCode);
            }
        }

        [Fact]
        public async Task Create_ShouldReturnWithId()
        {
            // Arrange
            using (var mock = AutoMock.GetLoose())
            {
                UserDto sampleDto = new UserDto("Keanu Reeves");
                UserDto sampleDtoWithId = new UserDto("Keanu Reeves") { Id = "99" };
                mock.Mock<IUserManager>()
                    .Setup(mm => mm.CreateUser(sampleDto))
                    .ReturnsAsync(sampleDtoWithId);

                var controller = mock.Create<UserController>();

                // Act
                var result = await controller.Post(sampleDto);
                var createdObject = ((ObjectResult)result.Result).Value as UserDto;
                string resultJson = JsonSerializer.Serialize(createdObject);
                string expectedJson = JsonSerializer.Serialize(sampleDtoWithId);

                // Assert
                Assert.True(createdObject != null); // Null check
                Assert.IsType<ObjectResult>(result.Result);
                Assert.Equal(sampleDtoWithId.Id, createdObject.Id);
                Assert.Equal(expectedJson, resultJson);
            }
        }

        [Fact]
        public async Task Put_ShouldReturnWithChanges()
        {
            // Arrange
            using (var mock = AutoMock.GetLoose())
            {
                var sampleData = await SampleData();
                UserDto sampleUser = sampleData.ElementAt(1);
                sampleUser.Username = "Keanu Reeves";
                mock.Mock<IUserManager>()
                    .Setup(mm => mm.UpdateUser(sampleUser))
                    .ReturnsAsync(sampleUser);

                var controller = mock.Create<UserController>();

                // Act
                var result = await controller.Put(sampleUser.Id, sampleUser);
                var updatedObject = ((ObjectResult)result.Result).Value as UserDto;
                string resultJson = JsonSerializer.Serialize(updatedObject);
                string expectedJson = JsonSerializer.Serialize(sampleUser);

                // Assert
                Assert.True(updatedObject != null); // Null check
                Assert.Equal(resultJson, expectedJson);
            }
        }

        private async Task<IEnumerable<UserDto>> SampleData()
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
