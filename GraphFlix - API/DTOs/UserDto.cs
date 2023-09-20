namespace GraphFlix.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool CookieAccept { get; set; }

        public UserDto(string userName)
        {
            Username = userName;
            CookieAccept = false;
        }
    }
}
