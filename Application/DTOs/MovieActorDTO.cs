namespace Application.DTOs
{
    public class MovieActorDTO
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int ActorId {  get; set; }

        public string Role { get; set; } = string.Empty;
        public ActorDTO Actor { get; set; }
    }
}
