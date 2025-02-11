using Domain.Entities;

namespace Application.DTOs
{
    public  class MovieGenreDTO
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int GenreId { get; set; }

        public GenreDTO Genre { get; set; }
    }
}
