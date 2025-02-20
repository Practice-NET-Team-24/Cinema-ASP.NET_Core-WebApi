using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces.Services
{
    public interface ITicketService
    {
        Task<IEnumerable<Ticket>> GetTicketsByUserIdAsync(int userId);
        Task<IEnumerable<PlaceDTO>> GetAvailablePlacesAsync(int sessionId);
        Task<IEnumerable<TicketDTO>> GetTicketsForSessionAsync(int sessionId);
        Task<bool> CancelTicketAsync(int ticketId);
        Task<IEnumerable<HallDTO>> GetHallsAsync();
        Task<bool> PurchaseTicketAsync(int userId, int sessionId, int rowNumber, int seatNumber);
    }
}
