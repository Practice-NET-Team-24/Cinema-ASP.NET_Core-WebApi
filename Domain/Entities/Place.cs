
using Domain.Interfaces;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Place : IEntity
    {
        public int Id { get; set; }
        public int HallId { get; set; }
        public Hall Hall { get; set; }
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
