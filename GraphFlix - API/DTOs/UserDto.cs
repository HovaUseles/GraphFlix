namespace GraphFlix.DTOs
{
    public class UserDto
    {
        public string Id { get; }
        public string Username { get; set; }
        public string Password { get; set; }
		public bool CookieAccept { get; set; }
    }
}
