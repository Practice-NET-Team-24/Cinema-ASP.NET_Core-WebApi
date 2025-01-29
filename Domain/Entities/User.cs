using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public enum Role {
        Admin,
        Client
    }
    public class User
    {
        public int Id { get; set; }
        public Role Role { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
