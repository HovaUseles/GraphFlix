using GraphFlix.DTOs;

namespace GraphFlix.Repositories
{
    public interface IUserRepository
    {

        public Task<IEnumerable<UserDto>> GetAll();

        public Task<UserDto?> GetById(int id);

        public Task<string?> GetUserSalt(string username);

        public Task<bool> TryVerifyLogin(string username, string passwordHash, out UserDto user);

        public Task Create(LoginDto user);

        public Task Update(int id, UserDto userChanges);

        public Task Delete(int id);
    }
}
