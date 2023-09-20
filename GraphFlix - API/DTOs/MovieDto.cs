using GraphFlix.Models;

namespace GraphFlix.DTOs
{
    public class MovieDto
    {

        public string? Id { get; set; }
        public string Title { get; set; }
        public DateOnly ReleaseDate { get; set; }

        public List<Genre> Genres { get; set; }

        public MovieDto(string title, DateOnly releaseDate, List<Genre> genres) 
        {
            this.Title = title;
            this.ReleaseDate = releaseDate;
            this.Genres = genres;
        }
    }
}
