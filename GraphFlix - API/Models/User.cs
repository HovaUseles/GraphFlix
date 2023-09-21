﻿namespace GraphFlix.Models
{
    public class User
    {
        public string Id { get; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public bool CookieAccept { get; set; }

        public User(
            string id,
            string userName,
            string passwordHash)
        {
            Id = id;
            UserName = userName;
            PasswordHash = passwordHash;
            CookieAccept = false;
        }
    }
}
