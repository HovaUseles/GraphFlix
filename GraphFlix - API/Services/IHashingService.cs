namespace GraphFlix.Services
{
    public interface IHashingService
    {
        public string HashPassword(string password, string salt);
    }
}
