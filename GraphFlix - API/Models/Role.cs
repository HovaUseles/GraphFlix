namespace GraphFlix.Models
{
    public class Role
    {
        public string Id { get; }
        public string Name { get; set; }

        public Role(
            string id,
            string name)
        {
            Id = id;
            Name = name;
        }
    }
}
