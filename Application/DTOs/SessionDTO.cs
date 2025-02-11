namespace Application.DTOs
{
    public class SessionDTO
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int HallId { get; set; }
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeEnd { get; set; }
        public float Price { get; set; }
        public int ReservedPlaces { get; set; }
    }
}
