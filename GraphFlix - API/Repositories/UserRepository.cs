using GraphFlix.DTOs;

namespace GraphFlix.Repositories;

public class UserRepository : IUserRepository
{
    public Task<UserDto> Create(UserDto user)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto> Delete(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserDto>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<UserDto?> GetById(string id)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto> Update(UserDto userChanges)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto> Update(object userChanges)
    {
        throw new NotImplementedException();
    }
}
