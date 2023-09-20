namespace GraphFlix___API___Tests
{
    public class MovieManagerTests
    {
		[Fact]
		public async Task GetMovies_ShouldReturnMovies()
		{
			using (var mock = AutoMock.GetLoose())
			{
				// Arrange
				mock.Mock<IMovieRepository>()
					.Setup(repo => repo.GetAll())
					.ReturnsAsync(SampleData());

				var manager = mock.Create<MovieManager>();

                // Act
                var result = await manager.GetMovies();
                string actualJson = JsonSerializer.Serialize(result);
                string expectedJson = JsonSerializer.Serialize(SampleData());

				// Assert
                Assert.True(result != null);
                Assert.Equal(expectedJson, actualJson);
			}
		}
        
        [Fact]
		public async Task GetMovie_ShouldReturnMovie()
		{
			using (var mock = AutoMock.GetLoose())
			{
                // Arrange
                var sampleMovie = SampleData().ElementAt(1);
				mock.Mock<IMovieRepository>()
					.Setup(repo => repo.GetById(sampleMovie.Id))
					.ReturnsAsync(sampleMovie);

				var manager = mock.Create<MovieManager>();

                // Act
                var result = await manager.GetMovie(sampleMovie.Id);
                var expected = sampleMovie;

				// Assert
                Assert.True(result != null);
                Assert.Equal(expected, result);
			}
		}

        [Fact]
        public async Task UpdateMovie_ShouldReturnWithChanges()
        {
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                MovieDto sampleMovie = SampleData().ElementAt(1);
                DateOnly dateChange = DateOnly.Parse("2012-01-01");
                sampleMovie.ReleaseDate = dateChange;

                mock.Mock<IMovieRepository>()
                    .Setup(repo => repo.Update(sampleMovie))
                    .ReturnsAsync(sampleMovie);

                var manager = mock.Create<MovieManager>();

                // Act
                var result = await manager.UpdateMovie(sampleMovie);

                // Assert
                Assert.True(result != null);
                Assert.Equal(sampleMovie, result);
                Assert.Equal(dateChange, result.ReleaseDate);
            }
        }

        private IEnumerable<MovieDto> SampleData()
        {
            List<Genre> genres = new List<Genre> { new Genre { Id = "1", Name = "Action" }, new Genre { Id = "2", Name = "Drama" } };

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
