using GraphFlix.DTOs;

namespace GraphFlix.Repositories
{
    public interface IMovieRepository
    {
        public Task<IEnumerable<MovieDto>> GetAll();

        public Task<MovieDto?> GetById(string id);

        public Task<MovieDto> Create(MovieDto movie);

        public Task<MovieDto> Update(MovieDto movieChanges);

        public Task<MovieDto> Delete(string id);

        public Task<IEnumerable<MovieDto>> GetRecommendedMovies(int userId);
    }
}
