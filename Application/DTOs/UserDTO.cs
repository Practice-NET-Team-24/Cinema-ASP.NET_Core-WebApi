using Domain.Enums;
namespace Application.DTOs
{
    
    public class UserDTO
    {
        public int Id { get; set; }
        public UserRole Role { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
