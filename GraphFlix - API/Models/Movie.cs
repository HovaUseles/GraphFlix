namespace GraphFlix.Models
{
    public class Movie
    {
        public string Id { get; }
        public string Name { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public Genre Genre { get; set; }
    }
}
