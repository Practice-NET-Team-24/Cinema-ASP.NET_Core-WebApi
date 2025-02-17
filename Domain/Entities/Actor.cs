using Domain.Interfaces;
namespace Domain.Entities
{
    public class Actor : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Surname { get; set; } = String.Empty;
        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
    }
}
