using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Specifications;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly IRepository<Ticket> _ticketRepository;
        private readonly IRepository<Place> _placeRepository;
        private readonly IRepository<Hall> _hallRepository;
        private readonly IRepository<Session> _sessionRepository;
        private readonly IMapper _mapper;

        public TicketService(
            IRepository<Ticket> ticketRepository,
            IRepository<Place> placeRepository,
            IRepository<Hall> hallRepository,
            IRepository<Session> sessionRepository,
            IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _placeRepository = placeRepository;
            _hallRepository = hallRepository;
            _sessionRepository = sessionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TicketDTO>> GetTicketsByUserIdAsync(int userId)
        {
            var spec = new Tickets.ByUserId(userId);
            var tickets = await _ticketRepository.GetListBySpecAsync(spec);
            return _mapper.Map<IEnumerable<TicketDTO>>(tickets);
        }

        public async Task<IEnumerable<PlaceDTO>> GetAvailablePlacesAsync(int sessionId)
        {
            var spec = new Places.ByAvailability(sessionId);
            var places = await _placeRepository.GetListBySpecAsync(spec);
            return _mapper.Map<IEnumerable<PlaceDTO>>(places);
        }

        public async Task<IEnumerable<HallDTO>> GetHallsAsync()
        {
            var halls = await _hallRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<HallDTO>>(halls);
        }

        public async Task<bool> PurchaseTicketAsync(int userId, int sessionId, int placeId)
        {
            var session = await _sessionRepository.GetByIdAsync(sessionId);
            if (session == null)
            {
                return false; 
            }


            var place = await _placeRepository.GetByIdAsync(placeId);
            if (place == null || place.HallId != session.HallId)
            {
                return false; // Place doesn't exist or doesn't belong to the selected hall
            }

            // Check if the seat is already reserved
            var ticket = await _ticketRepository.GetListBySpecAsync(new Tickets.BySessionAndPlaceId(sessionId, placeId));
            if (ticket.Any())
            {
                return false; // Seat is already reserved
            }

            // Create the ticket
            var newTicket = new Ticket
            {
                PlaceId = placeId,
                SessionId = sessionId,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            // Save the ticket
            await _ticketRepository.InsertAsync(newTicket);

            // Update session's reserved places
            session.ReservedPlaces++;
            await _sessionRepository.UpdateAsync(session);

            return true;
        }
        public async Task<bool> PurchaseTicketAsync(int userId, int sessionId, int rowNumber, int seatNumber)
        {
            // Check if the session exists
            var session = await _sessionRepository.GetByIdAsync(sessionId);
            if (session == null)
            {
                return false; // Session doesn't exist
            }

            // Check if the place exists in the session's hall
            var place = await _placeRepository.GetListBySpecAsync(new Tickets.PlaceByCoords(session.HallId, rowNumber, seatNumber));
            if (place == null || !place.Any())
            {
                return false; // Place doesn't exist in the hall or invalid seat
            }

            var selectedPlace = place.First(); // Assuming the place exists and we only expect one match

            // Check if the selected seat is already reserved
            var existingTicket = await _ticketRepository.GetListBySpecAsync(new Tickets.BySessionAndPlaceId(sessionId, selectedPlace.Id));
            if (existingTicket.Any())
            {
                return false; // Seat is already reserved
            }

            // Create the ticket
            var newTicket = new Ticket
            {
                PlaceId = selectedPlace.Id,
                SessionId = sessionId,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            // Save the ticket
            await _ticketRepository.InsertAsync(newTicket);

            // Update session's reserved places count
            session.ReservedPlaces++;
            await _sessionRepository.UpdateAsync(session);

            return true; // Ticket successfully purchased
        }


    }
}
