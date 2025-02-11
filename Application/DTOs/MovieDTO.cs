namespace Application.DTOs
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public string TrailerURL { get; set; }
        public int AgeRestriction { get; set; }
        public int Duration { get; set; }
        public float Rating { get; set; }
        public IEnumerable<MovieActorDTO> MovieActors { get; set; }
        public IEnumerable<MovieGenreDTO> MovieGenres { get; set; }

    }
}
