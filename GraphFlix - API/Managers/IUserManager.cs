using GraphFlix.DTOs;

namespace GraphFlix.Managers
{
    public interface IUserManager
    {

        public Task<IEnumerable<UserDto>> GetUsers();

        public Task<UserDto> GetUser(string id);

        public Task<UserDto> CreateUser(UserDto user);

        public Task<UserDto> UpdateUser(UserDto userChanges);

        public Task<UserDto> DeleteUser(string id);

    }
}
