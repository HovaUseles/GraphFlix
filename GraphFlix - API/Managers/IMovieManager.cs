using GraphFlix.DTOs;

namespace GraphFlix.Managers
{
    public interface IMovieManager
    {

        public Task<IEnumerable<MovieDto>> GetMovies();

        public Task<MovieDto> GetMovie(string id);

        public Task<MovieDto> CreateMovie(MovieDto movie);

        public Task<MovieDto> UpdateMovie(MovieDto movieChanges);

        public Task<MovieDto> DeleteMovie(string id);

        public Task<IEnumerable<MovieDto>> GetRecommendedMovies(UserDto user);
    }
}
