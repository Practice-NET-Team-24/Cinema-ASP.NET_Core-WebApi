using Ardalis.Specification;
using Domain.Entities;
using System;

namespace Application.Specifications
{
    public static class Sessions
    {
        public class ById : Specification<Session>
        {
            public ById(int sessionId)
            {
                Query
                    .Where(s => s.Id == sessionId);
            }
        }

        public class ByIds : Specification<Session>
        {
            public ByIds(int[] sessionIds)
            {
                Query
                    .Where(s => sessionIds.Contains(s.Id));
            }
        }

        public class AllByMovieId : Specification<Session>
        {
            public AllByMovieId(int movieId)
            {
                Query
                    .Where(s => s.MovieId == movieId);
            }
        }

        public class AllByHallId : Specification<Session>
        {
            public AllByHallId(int hallId)
            {
                Query
                    .Where(s => s.HallId == hallId);
            }
        }

        // New: ByDateRange
        public class ByDateRange : Specification<Session>
        {
            public ByDateRange(DateTime fromDate, DateTime toDate)
            {
                Query
                    .Where(s => s.DateTimeStart >= fromDate && s.DateTimeStart <= toDate);
            }
        }
    }
}
