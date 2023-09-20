using GraphFlix.DTOs;
using GraphFlix.Models;

namespace GraphFlix.Repositories
{
    public class MockMovieRepository : IMovieRepository
    {
        public Task<MovieDto> Create(MovieDto movie)
        {
            throw new NotImplementedException();
        }

        public Task<MovieDto> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MovieDto>> GetAll()
        {
            List<Genre> genres = new List<Genre> { new Genre { Id = "1", Name = "Action" }, new Genre { Id = "2", Name = "Drama" } };
            return new List<MovieDto>()
            {
                new MovieDto("Inception", DateOnly.Parse("2010-01-01"), genres) { Id = "1" },
                new MovieDto("Fight club", DateOnly.Parse("2010-01-01"), genres) { Id = "2" },
                new MovieDto("Da vinci Code", DateOnly.Parse("2010-01-01"), genres) { Id = "3" },
                new MovieDto("The Shawshank Redemption", DateOnly.Parse("2010-01-01"), genres) { Id = "4" },
                new MovieDto("The Godfather", DateOnly.Parse("2010-01-01"), genres) { Id = "5" }
            };
        }

        public Task<MovieDto?> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MovieDto>> GetRecommendedMovies(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<MovieDto> Update(MovieDto movieChanges)
        {
            throw new NotImplementedException();
        }
    }
}
