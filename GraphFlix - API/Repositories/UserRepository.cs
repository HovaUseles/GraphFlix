using GraphFlix.DTOs;

namespace GraphFlix.Repositories;

public class UserRepository : IUserRepository
{
    public Task<UserDto> Create(LoginDto user)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserDto>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<UserDto?> GetById(int id)
    {
        throw new NotImplementedException();
    }
    public Task<string?> GetUserSalt(string username)
    {
        throw new NotImplementedException();
    }

    public Task<bool> TryVerifyLogin(string username, string passwordHash, out UserDto user)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto> Update(int id, UserDto userChanges)
    {
        throw new NotImplementedException();
    }
}
