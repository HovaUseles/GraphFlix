namespace GraphFlix___API___Tests
{
    public class MovieControllerTests
    {
        List<Genre> genres = new List<Genre> { new Genre { Id = "1", Name = "Action" }, new Genre { Id = "2", Name = "Drama" } };

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
                IEnumerable<MovieDto> sampleDtos = await SampleData();

                string actualMoviesJson = JsonSerializer.Serialize(listOfMovies);
                string expectedMoviesJson = JsonSerializer.Serialize(sampleDtos);

                // Assert
                Assert.True( result != null ); // Null check
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
                MovieDto sampleMovie = sampleData.ElementAt(1);
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
                MovieDto? movieDto = null;

                mock.Mock<IMovieManager>()
                    .Setup(mm => mm.GetMovie(emptyId))
                    .ReturnsAsync(movieDto);

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
                MovieDto sampleDto = new MovieDto("The Matrix", DateOnly.Parse("2010-01-01"), genres);
                MovieDto sampleDtoWithId = new MovieDto("The Matrix", DateOnly.Parse("2010-01-01"), genres) { Id = "99"};
                mock.Mock<IMovieManager>()
                    .Setup(mm => mm.CreateMovie(sampleDto))
                    .ReturnsAsync(sampleDtoWithId);

                var controller = mock.Create<MovieController>();

                // Act
                var result = await controller.Post(sampleDto);
                var createdObject = ((ObjectResult)result.Result).Value as MovieDto;
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
                MovieDto sampleMovie = sampleData.ElementAt(1);
                DateOnly dateChange = DateOnly.Parse("2012-01-01");
                sampleMovie.ReleaseDate = dateChange;
                mock.Mock<IMovieManager>()
                    .Setup(mm => mm.UpdateMovie(sampleMovie))
                    .ReturnsAsync(sampleMovie);

                var controller = mock.Create<MovieController>();

                // Act
                var result = await controller.Put(sampleMovie.Id, sampleMovie);
                var updatedObject = ((ObjectResult)result.Result).Value as MovieDto;
                string resultJson = JsonSerializer.Serialize(updatedObject);
                string expectedJson = JsonSerializer.Serialize(sampleMovie);

                // Assert
                Assert.True(updatedObject != null); // Null check
                Assert.Equal(resultJson, expectedJson);
            }
        }

        private async Task<IEnumerable<MovieDto>> SampleData()
        {
            List<MovieDto> samples = new List<MovieDto>()
            {
                new MovieDto("Inception", DateOnly.Parse("2010-01-01"), genres) { Id = "1" },
                new MovieDto("Fight club", DateOnly.Parse("2010-01-01"), genres) { Id = "2" },
                new MovieDto("Da vinci Code", DateOnly.Parse("2010-01-01"), genres) { Id = "3" },
                new MovieDto("The Shawshank Redemption", DateOnly.Parse("2010-01-01"), genres) { Id = "4" },
                new MovieDto("The Godfather", DateOnly.Parse("2010-01-01"), genres) { Id = "5" }
            };
            return samples;
        }
    }
}
