using GraphFlix.DTOs;

namespace GraphFlix.Repositories
{
    public interface IUserRepository
    {

        public Task<IEnumerable<UserDto>> GetAll();

        public Task<UserDto?> GetById(int id);

        public Task<string?> GetUserSalt(string username);

        public Task<bool> TryVerifyLogin(string username, string passwordHash, out UserDto user);

        public Task<UserDto> Create(LoginDto user);

        public Task<UserDto> Update(int id, UserDto userChanges);

        public Task<UserDto> Delete(int id);
    }
}
