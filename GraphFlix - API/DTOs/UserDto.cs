namespace GraphFlix.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public bool CookieAccept { get; set; }

        public UserDto(string userName)
        {
            UserName = userName;
            CookieAccept = false;
        }
    }
}
