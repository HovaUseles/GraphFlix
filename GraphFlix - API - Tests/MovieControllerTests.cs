namespace GraphFlix___API___Tests
{
    public class MovieControllerTests
    {
        List<Genre> genres = new List<Genre> { new Genre { Id = 1, Name = "Action" }, new Genre { Id = 2, Name = "Drama" } };

        [Fact]
        public async Task Get_ShouldReturnMovies()
        {
            // Arrange
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<IMovieRepository>()
                    .Setup(mr => mr.GetAll())
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
                mock.Mock<IMovieRepository>()
                    .Setup(mr => mr.GetById(sampleMovie.Id))
                    .ReturnsAsync(sampleMovie);

                var controller = mock.Create<MovieController>();

                // Act
                var result = await controller.Get(sampleMovie.Id);
                var retmrnedMovie = (result.Result as ObjectResult).Value;

                string actualMovieJson = JsonSerializer.Serialize(retmrnedMovie);
                string expectedMovieJson = JsonSerializer.Serialize(sampleMovie);

                // Assert
                Assert.True(result != null); // Null check
                Assert.Equal(expectedMovieJson, actualMovieJson);
            }
        }

        private async Task<IEnumerable<MovieDto>> SampleData()
        {
            List<MovieDto> samples = new List<MovieDto>()
            {
                new MovieDto("Inception", DateOnly.Parse("2010-01-01"), genres) { Id = 1 },
                new MovieDto("Fight club", DateOnly.Parse("2010-01-01"), genres) { Id = 2 },
                new MovieDto("Da vinci Code", DateOnly.Parse("2010-01-01"), genres) { Id = 3 },
                new MovieDto("The Shawshank Redemption", DateOnly.Parse("2010-01-01"), genres) { Id = 4 },
                new MovieDto("The Godfather", DateOnly.Parse("2010-01-01"), genres) { Id = 5 }
            };
            return samples;
        }
    }
}
