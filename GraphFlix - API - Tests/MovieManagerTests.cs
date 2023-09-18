using Autofac.Extras.Moq;
using GraphFlix.Controllers;
using GraphFlix.DTOs;
using GraphFlix.Managers;
using GraphFlix.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                var expected = SampleData();

				// Assert
                Assert.True(result != null);
                Assert.Equal(expected.Count(), result.Count());
                for (int i = 0; i < result.Count(); i++)
                {
                    Assert.Equal(expected.ElementAt(i).Id, result.ElementAt(i).Id);
                    Assert.Equal(expected.ElementAt(i).Title, result.ElementAt(i).Title);
                    Assert.Equal(expected.ElementAt(i).Description, result.ElementAt(i).Description);
                    Assert.Equal(expected.ElementAt(i).ReleaseDate, result.ElementAt(i).ReleaseDate);
                }
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
            List<MovieDto> samples = new List<MovieDto>()
            {
                new MovieDto("Inception", "Description1", DateOnly.Parse("2010-01-01")) { Id = "1" },
                new MovieDto("Fight club", "Description2", DateOnly.Parse("2010-01-01")) { Id = "2" },
                new MovieDto("Da vinci Code", "Description3", DateOnly.Parse("2010-01-01")) { Id = "3" },
                new MovieDto("The Shawshank Redemption", "Description4", DateOnly.Parse("2010-01-01")) { Id = "4" },
                new MovieDto("The Godfather", "Description5", DateOnly.Parse("2010-01-01")) { Id = "5" }
            };
            return samples;
        }
    }
}
