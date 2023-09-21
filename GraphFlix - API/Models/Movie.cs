namespace GraphFlix.Models
{
    public class Movie
    {
        public int Id { get; }
        public string Title { get; set; }
        public DateOnly ReleaseDate { get; set; }
    }
}
