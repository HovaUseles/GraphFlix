namespace GraphFlix.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
		public bool CookieAccept { get; set; }
        public IEnumerable<RoleDto> Roles { get; set; }
    }
}
