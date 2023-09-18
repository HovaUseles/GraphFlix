using GraphFlix.DTOs;
using GraphFlix.Repositories;

namespace GraphFlix.Managers
{
    public class UserManager : IUserManager
    {
        private IUserRepository _userRepo { get; }

        public UserManager(IUserRepository UserRepo)
        {
            _userRepo = UserRepo;
        }


        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            return await _userRepo.GetAll();
        }


        public async Task<UserDto> GetUser(string id)
        {
            return await _userRepo.GetById(id);
        }


        public async Task<UserDto> CreateUser(UserDto user)
        {
            return await _userRepo.Create(user);
        }


        public async Task<UserDto> UpdateUser(UserDto userChanges)
        {
            return await _userRepo.Update(userChanges);
        }


        public async Task<UserDto> DeleteUser(string id)
        {
            return await _userRepo.Delete(id);
        }
    }
}
