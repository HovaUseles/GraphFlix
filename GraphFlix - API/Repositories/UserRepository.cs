using GraphFlix.Database;
using GraphFlix.DTOs;
using GraphFlix.Models;

namespace GraphFlix.Repositories;

public class UserRepository : IUserRepository
{
    private readonly INeo4J neo;
    public UserRepository(INeo4J neo4j)
    {
        neo = neo4j;
    }
    public async Task Create(LoginDto userDto)
    {
        //TODO: Exception handling
        User user = new User() 
        {
            UserName = userDto.Username,

            //TODO: Hash
            PasswordHash = userDto.Password
        };


        IQuery q1 = new Query().PlainQuery("");
        await neo.ExecuteWriteAsync(q1);
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<UserDto>> GetAll()
    {
        IQuery q1 = new Query().PlainQuery("");
        var result = await neo.ExecuteReadAsync<UserDto>(q1);
        return result;
    }

    public async Task<UserDto?> GetById(int id)
    {
        IQuery q1 = new Query().PlainQuery("");
        var result = await neo.ExecuteReadAsync<UserDto>(q1);
        return result.FirstOrDefault();
    }
    public Task<string?> GetUserSalt(string username)
    {
        throw new NotImplementedException();
    }

    public Task<bool> TryVerifyLogin(string username, string passwordHash, out UserDto user)
    {
        throw new NotImplementedException();
    }

    public Task Update(int id, UserDto userChanges)
    {
        throw new NotImplementedException();
    }
}
