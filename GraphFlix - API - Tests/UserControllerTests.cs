using Autofac.Extras.Moq;
using GraphFlix.Controllers;
using GraphFlix.DTOs;
using GraphFlix.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphFlix___API___Tests
{
    internal class UserControllerTests
    {
        [Fact]
        public async Task Get_ShouldReturnMovies()
        {
            // Arrange
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IMovieManager>()
                    .Setup(mm => mm.GetMovies())
                    .ReturnsAsync(await SampleData());

                var controller = mock.Create<MovieController>();

                // Act
                var result = await controller.Get();
                var listOfMovies = (result.Result as ObjectResult).Value;
                IEnumerable<UserDto> sampleDtos = await SampleData();

                string actualMoviesJson = JsonSerializer.Serialize(listOfMovies);
                string expectedMoviesJson = JsonSerializer.Serialize(sampleDtos);

                // Assert
                Assert.True(result != null); // Null check
                Assert.Equal(expectedMoviesJson, actualMoviesJson);
            }
        }

        [Fact]
        public async Task GetById_ShouldReturnMovie()
        {
            // Arrange
            using (var mock = AutoMock.GetLoose())
            {
                var sampleData = await SampleData();
                UserDto sampleMovie = sampleData.ElementAt(1);
                mock.Mock<IMovieManager>()
                    .Setup(mm => mm.GetMovie(sampleMovie.Id))
                    .ReturnsAsync(sampleMovie);

                var controller = mock.Create<MovieController>();

                // Act
                var result = await controller.Get(sampleMovie.Id);
                var returnedMovie = (result.Result as ObjectResult).Value;

                string actualMovieJson = JsonSerializer.Serialize(returnedMovie);
                string expectedMovieJson = JsonSerializer.Serialize(sampleMovie);

                // Assert
                Assert.True(result != null); // Null check
                Assert.Equal(expectedMovieJson, actualMovieJson);
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

                mock.Mock<IMovieManager>()
                    .Setup(mm => mm.GetMovie(emptyId))
                    .ReturnsAsync(UserDto);

                var controller = mock.Create<MovieController>();

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
                UserDto sampleDto = new UserDto("The Matrix", "The Matrix Description", DateOnly.Parse("2010-01-01"));
                UserDto sampleDtoWithId = new UserDto("The Matrix", "The Matrix Description", DateOnly.Parse("2010-01-01")) { Id = "99" };
                mock.Mock<IMovieManager>()
                    .Setup(mm => mm.CreateMovie(sampleDto))
                    .ReturnsAsync(sampleDtoWithId);

                var controller = mock.Create<MovieController>();

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
                UserDto sampleMovie = sampleData.ElementAt(1);
                DateOnly dateChange = DateOnly.Parse("2012-01-01");
                sampleMovie.ReleaseDate = dateChange;
                mock.Mock<IMovieManager>()
                    .Setup(mm => mm.UpdateMovie(sampleMovie))
                    .ReturnsAsync(sampleMovie);

                var controller = mock.Create<MovieController>();

                // Act
                var result = await controller.Put(sampleMovie.Id, sampleMovie);
                var updatedObject = ((ObjectResult)result.Result).Value as UserDto;
                string resultJson = JsonSerializer.Serialize(updatedObject);
                string expectedJson = JsonSerializer.Serialize(sampleMovie);

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
