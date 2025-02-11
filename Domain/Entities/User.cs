
using System.Collections.Generic;
using Domain.Enums;
using Domain.Interfaces;

namespace Domain.Entities
{
    
    public class User : IEntity
    {
        public int Id { get; set; }
        public UserRole Role { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
