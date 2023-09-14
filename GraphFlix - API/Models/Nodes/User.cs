using GraphFlix.Interfaces;
using GraphFlix.Models.Edges;

namespace GraphFlix.Models.Nodes
{
    public class User
    {
        public string Id { get; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public bool CookieAccept { get; set; }
        public Role Role { get; set; }

        public User(
            string id,
            string userName,
            string passwordHash,
            Role role)
        {
            Id = id;
            UserName = userName;
            PasswordHash = passwordHash;
            CookieAccept = false;
            Role = role;
        }
    }
}
