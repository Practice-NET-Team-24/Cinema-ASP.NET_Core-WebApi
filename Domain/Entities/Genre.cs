
using Domain.Interfaces;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Genre : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public ICollection<MovieGenre> MovieGenres { get; set; }
    }
}
