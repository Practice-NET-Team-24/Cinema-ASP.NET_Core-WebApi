

using Domain.Interfaces;

namespace Domain.Entities
{
    public class MovieActor : IEntity
    {
        public int Id { get; set; }
        public int MovieId { get; set; }

        public int ActorId {  get; set; }
        public string Role { get; set; } = String.Empty;
        public Movie Movie { get; set; }
        public Actor Actor { get; set; }
    }
}
