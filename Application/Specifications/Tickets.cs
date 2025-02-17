using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications
{
    public static class Tickets
    {
        public class ById : Specification<Ticket>
        {
            public ById(int ticketId)
            {
                Query.Where(t => t.Id == ticketId);
            }
        }

        public class ByUserId : Specification<Ticket>
        {
            public ByUserId(int userId)
            {
                Query.Where(t => t.UserId == userId);
            }
        }
        public class BySessionId : Specification<Ticket>
        {
            public BySessionId(int sessionId)
            {
                Query.Where(t => t.SessionId == sessionId);
            }
        }

        public class BySessionAndPlaceId : Specification<Ticket>
        {
            public BySessionAndPlaceId(int sessionId, int placeId)
            {
                Query.Where(t => t.SessionId == sessionId && t.PlaceId == placeId);
            }
        }

        public class PlaceByCoords : Specification<Place>
        {
            public PlaceByCoords(int hallId, int rowNumber, int seatNumber)
            {
                Query.Where(p => p.HallId == hallId && p.RowNumber == rowNumber && p.SeatNumber == seatNumber);
            }
        }
    }
}
