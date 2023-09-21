namespace GraphFlix.Models
{
    public class User
    {
        public string Id { get; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public bool CookieAccept { get; set; }

    }
}
