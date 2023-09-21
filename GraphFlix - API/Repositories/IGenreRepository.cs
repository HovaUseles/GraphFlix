using GraphFlix.DTOs;

namespace GraphFlix.Repositories
{
    public interface IGenreRepository
    {
        public Task<IEnumerable<GenreDto>> GetAll();
    }
}
