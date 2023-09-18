using GraphFlix.DTOs;
using GraphFlix.Repositories;

namespace GraphFlix.Managers
{
    public class MovieManager : IMovieManager
    {
        private IMovieRepository _movieRepo { get; }

        public MovieManager(IMovieRepository movieRepo)
        {
            _movieRepo = movieRepo;
        }


        public async Task<IEnumerable<MovieDto>> GetMovies()
        {
            return await _movieRepo.GetAll();
        }


        public async Task<MovieDto> GetMovie(string id)
        {
            return await _movieRepo.GetById(id);
        }


        public async Task<MovieDto> CreateMovie(MovieDto movie)
        {
            return await _movieRepo.Create(movie);
        }


        public async Task<MovieDto> UpdateMovie(MovieDto movieChanges)
        {
            return await _movieRepo.Update(movieChanges);
        }


        public async Task<MovieDto> DeleteMovie(string id)
        {
            return await _movieRepo.Delete(id);
        }


        public async Task<IEnumerable<MovieDto>> GetRecommendedMovies(UserDto user)
        {
            throw new NotImplementedException();
        }

    }
}
