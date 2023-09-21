using GraphFlix.DTOs;

namespace GraphFlix.Repositories
{
    public interface IMovieRepository
    {
        public Task<IEnumerable<MovieDto>> GetAll();

        public Task<MovieDto?> GetById(int id);

        public Task Create(MovieDto movie);

        public Task Update(int id, MovieDto movieChanges);

        public Task Delete(int id);

        public Task<IEnumerable<MovieDto>> GetRecommendedMovies(int userId);
    }
}
