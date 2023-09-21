using GraphFlix.DTOs;

namespace GraphFlix.Repositories
{
    public interface IMovieRepository
    {
        public Task<IEnumerable<MovieDto>> GetAll();

        public Task<MovieDto?> GetById(string id);

        public Task Create(MovieDto movie);

        public Task Update(MovieDto movieChanges);

        public Task Delete(string id);

        public Task<IEnumerable<MovieDto>> GetRecommendedMovies(int userId);
    }
}
