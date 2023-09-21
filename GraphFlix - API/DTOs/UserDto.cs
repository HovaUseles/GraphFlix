namespace GraphFlix.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
		public bool CookieAccept { get; set; }
        public IEnumerable<RoleDto> Roles { get; set; }
    }
}
