using Application.DTOs;
using Application.Interfaces.Services;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Application.Specifications;

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


        public async Task<bool> PurchaseTicketAsync(int userId, int sessionId, int rowNumber, int seatNumber)
        {

            var session = await _sessionRepository.GetByIdAsync(sessionId);
            if (session == null)
            {
                return false; 
            }


            var place = await _placeRepository.GetListBySpecAsync(new Tickets.PlaceByCoords(session.HallId, rowNumber, seatNumber));
            if (place == null || !place.Any())
            {
                return false;
            }

            var selectedPlace = place.First(); 

            
            var existingTicket = await _ticketRepository.GetListBySpecAsync(new Tickets.BySessionAndPlaceId(sessionId, selectedPlace.Id));
            if (existingTicket.Any())
            {
                return false; 
            }

            
            var newTicket = new Ticket
            {
                PlaceId = selectedPlace.Id,
                SessionId = sessionId,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            
            await _ticketRepository.InsertAsync(newTicket);

            
            session.ReservedPlaces++;
            await _sessionRepository.UpdateAsync(session);

            return true; 
        }

        
        public async Task<bool> CancelTicketAsync(int ticketId)
        {
            var ticket = await _ticketRepository.GetByIdAsync(ticketId);
            if (ticket == null)
            {
                return false;
            }

           
            await _ticketRepository.DeleteAsync(ticket);

           
            var session = await _sessionRepository.GetByIdAsync(ticket.SessionId);
            if (session != null)
            {
                session.ReservedPlaces--;
                await _sessionRepository.UpdateAsync(session);
            }

            return true;
        }

        
        public async Task<IEnumerable<TicketDTO>> GetTicketsForSessionAsync(int sessionId)
        {
            var tickets = await _ticketRepository.GetListBySpecAsync(new Tickets.BySessionId(sessionId));
            return _mapper.Map<IEnumerable<TicketDTO>>(tickets);
        }

        public async Task<IEnumerable<HallDTO>> GetHallsAsync()
        {
            var halls = await _hallRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<HallDTO>>(halls);
        }
    }
}
