using Domain.Interfaces;
using System;

namespace Domain.Entities
{
    public class Ticket : IEntity
    {
        public int Id { get; set; }
        public int PlaceId { get; set; }
        public int SessionId { get; set; }
        public int UserId { get; set; }
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
        public Place Place { get; set; }
        public Session Session { get; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
