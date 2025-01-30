using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Session
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int HallId { get; set; }
        public Movie Movie { get; set; }
        public Hall Hall { get; set; }
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeEnd { get; set; }
        public float Price { get; set; }
        public int ReservedPlaces { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
