using GraphFlix.DTOs;

namespace GraphFlix.Repositories
{
    public class MockUserRepository : IUserRepository
    {
        public Task Create(LoginDto user)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
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

        public Task<UserDto?> GetByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public Task<string?> GetUserSalt(string username)
        {
            throw new NotImplementedException();
        }


        public Task Update(int id, UserDto userChanges)
        {
            throw new NotImplementedException();
        }

        public Task<bool> VerifyLogin(string username, string passwordHash)
        {
            throw new NotImplementedException();
        }
    }
}
