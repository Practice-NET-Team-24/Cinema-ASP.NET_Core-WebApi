using Microsoft.AspNetCore.Mvc;
using Application.Interfaces.Services;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = User.Claims.First(c => c.Type == "userId").Value;
            var parsed = int.TryParse(userId, out int id);
            if (!parsed) return BadRequest();
            var tickets = await _ticketService.GetTicketsByUserIdAsync(id);
            return Ok(tickets);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserTickets(int userId)
        {
            var tickets = await _ticketService.GetTicketsByUserIdAsync(userId);
            return Ok(tickets);
        }

        [HttpGet("session/{sessionId}/available")]
        public async Task<IActionResult> GetAvailableSeats(int sessionId)
        {
            var availableSeats = await _ticketService.GetAvailablePlacesAsync(sessionId);
            return Ok(availableSeats);
        }

        [HttpPost("purchase")]
        public async Task<IActionResult> PurchaseTicket([FromBody] TicketDTO request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request.");
            }
            
            var userId = User.Claims.First(c => c.Type == "userId").Value;

            var isParsed = int.TryParse(userId, out int id);
            
            if (!isParsed) return BadRequest();
            
            var result = await _ticketService.PurchaseTicketAsync(id, request.SessionId, request.RowNumber,
                request.SeatNumber);

            if (result)
            {
                return Ok("Ticket successfully purchased.");
            }
            else
            {
                return BadRequest("Seat is already reserved or invalid.");
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{ticketId}/cancel")]
        public async Task<IActionResult> CancelTicket(int ticketId)
        {
            var result = await _ticketService.CancelTicketAsync(ticketId);

            if (result)
            {
                return Ok("Ticket successfully canceled.");
            }
            else
            {
                return NotFound("Ticket not found or already canceled.");
            }
        }

        [HttpGet("session/{sessionId}")]
        public async Task<IActionResult> GetTicketsForSession(int sessionId)
        {
            var tickets = await _ticketService.GetTicketsForSessionAsync(sessionId);
            return Ok(tickets);
        }
    }
}