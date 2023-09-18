using GraphFlix.DTOs;

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
            return new List<MovieDto>()
            {
                new MovieDto("Inception", "Description1", DateOnly.Parse("2010-01-01")) { Id = "1" },
                new MovieDto("Fight club", "Description2", DateOnly.Parse("2010-01-01")) { Id = "2" },
                new MovieDto("Da vinci Code", "Description3", DateOnly.Parse("2010-01-01")) { Id = "3" },
                new MovieDto("The Shawshank Redemption", "Description4", DateOnly.Parse("2010-01-01")) { Id = "4" },
                new MovieDto("The Godfather", "Description5", DateOnly.Parse("2010-01-01")) { Id = "5" }
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
