using GraphFlix.Database;
using GraphFlix.DTOs;
using GraphFlix.Models;
using GraphFlix.Services;
using Microsoft.AspNetCore.Identity;

namespace GraphFlix.Repositories;

public class UserRepository : IUserRepository
{
    private readonly INeo4J neo;
    private readonly IHashingService hashingService;
    private readonly ISaltService saltService;
    public UserRepository(INeo4J neo4j, IHashingService hashService, ISaltService saltService)
    {
        neo = neo4j;
        hashingService = hashService;
        this.saltService = saltService;
    }
    public async Task Create(LoginDto userDto)
    {
        string salt = saltService.GenerateSalt();
        string hash = hashingService.HashPassword(userDto.Password, salt);

        //TODO: Exception handling
        User user = new User()
        {
            UserName = userDto.Username,
            PasswordHash = hash
        };


        IQuery q1 = new Query().PlainQuery($$"""
            CREATE (u:User { 
                user_name: "{{user.UserName}}", 
                password_hash: "{{user.PasswordHash}}", 
                cookie_accept: {{user.CookieAccept}}, 
                salt: "{{salt}}" })
            WITH u
            MATCH (r:Role WHERE r.name = "Customer")
            WITH u, r
            CREATE (u)-[ir:IS_ROLE]->(r)
            RETURN u, ir, r
        """);
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
    public async Task<UserDto?> GetByUsername(string username)
    {
        IQuery q1 = new Query().PlainQuery($$"""
            MATCH (u:User WHERE u.user_name = "{{username}}")-[ir:IS_ROLE]->(r:Role) 
            WITH COLLECT({
                Id: split(elementId(r), ':')[2],
                Name: r.name
            }) as roles, u
            RETURN {
                Id: split(elementId(u), ':')[2],
                Username: u.user_name, 
                CookieAccept: u.cookie_accept,
                Salt: u.salt,
                Roles: roles
            } AS User LIMIT 1
            """);
        var result = await neo.ExecuteReadAsync<UserDto>(q1);
        return result.FirstOrDefault();
    }
    public async Task<string?> GetUserSalt(string username)
    {
        IQuery q1 = new Query().PlainQuery($"""MATCH (u:User WHERE u.user_name = "{username}") RETURN u.salt""");
        var result = await neo.ExecuteReadAsync<string>(q1);
        return result.FirstOrDefault();
    }

    public async Task<bool> VerifyLogin(string username, string passwordHash)
    {
        IQuery q1 = new Query().PlainQuery($"""MATCH (u:User WHERE u.user_name = "{username}" AND u.password_hash = "{passwordHash}") RETURN COUNT(u) LIMIT 1""");
        var result = await neo.ExecuteReadAsync<int>(q1);

        if( result.First() > 0 )
        {
            return true;
        }

        return false;
    }

    public Task Update(int id, UserDto userChanges)
    {
        throw new NotImplementedException();
    }
}
