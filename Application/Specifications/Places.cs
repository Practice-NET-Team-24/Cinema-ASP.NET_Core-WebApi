using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications
{
    public static class Places
    {
        public class ByHallId : Specification<Place>
        {
            public ByHallId(int hallId)
            {
                Query.Where(p => p.HallId == hallId);
            }
        }

        public class ByAvailability : Specification<Place>
        {
            public ByAvailability(int sessionId)
            {
                Query.Where(p => !p.Tickets.Any(t => t.SessionId == sessionId));
            }
        }
    }
}
