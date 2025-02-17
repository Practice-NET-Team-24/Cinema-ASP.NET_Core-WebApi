namespace Application.DTOs
{
    public class TicketDTO
    {
        public int Id { get; set; }
        public int PlaceId { get; set; }
        public int SessionId { get; set; }
        public int UserId { get; set; }
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
