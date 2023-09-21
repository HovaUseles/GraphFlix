using GraphFlix.DTOs;

namespace GraphFlix.Services
{
    public interface ITokenService
    {
        public TokenDto CreateToken(UserDto user);
    }
}
