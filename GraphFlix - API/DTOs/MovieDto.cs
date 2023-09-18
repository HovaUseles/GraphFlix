namespace GraphFlix.DTOs
{
    public class MovieDto
    {

        public string? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateOnly ReleaseDate { get; set; }

        public MovieDto(string title, string description, DateOnly releaseDate) 
        {
            this.Title = title;
            this.Description = description;
            this.ReleaseDate = releaseDate;
        }
    }
}
