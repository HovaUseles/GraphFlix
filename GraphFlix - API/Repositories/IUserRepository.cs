using GraphFlix.DTOs;

namespace GraphFlix.Repositories
{
    public interface IUserRepository
    {

        public Task<IEnumerable<UserDto>> GetAll();

        public Task<UserDto?> GetById(string id);

        public Task<UserDto> Create(UserDto user);

        public Task<UserDto> Update(UserDto userChanges);

        public Task<UserDto> Delete(string id);
        Task<UserDto> Update(object userChanges);
    }
}
