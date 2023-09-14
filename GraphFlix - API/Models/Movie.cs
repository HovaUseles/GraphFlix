namespace GraphFlix.Models
{
    public class Movie
    {
        public string Id { get; }
        public string Title { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public Genre Genre { get; set; }
    }
}
