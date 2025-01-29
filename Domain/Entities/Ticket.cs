using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public int PlaceId { get; set; }
        public int SessionId { get; set; }
        public int UserId { get; set; }
        public Place Place { get; set; }
        public Session Session { get; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
