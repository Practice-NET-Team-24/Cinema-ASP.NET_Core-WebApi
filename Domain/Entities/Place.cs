using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Place
    {
        public int Id { get; set; }
        public int HallId { get; set; }
        public Hall Hall { get; set; }
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
        public ICollection<Ticket> Tickets { get;
    }
}
