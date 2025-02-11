using Domain.Interfaces;
namespace Domain.Entities
{
    public class Hall : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Place> Places { get; set; }
        public ICollection<Session> Sessions { get; set; }
    }
}
