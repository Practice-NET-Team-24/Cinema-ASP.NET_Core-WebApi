using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications
{
    public static class Movies
    {
        public class ById : Specification<Movie>
        {
            public ById(int movieId)
            {
                Query
                    .Where(m => m.Id == movieId)
                    .Include(m => m.MovieActors)
                    .Include(m => m.MovieGenres);
            }
        }

        public class ByIds : Specification<Movie>
        {
            public ByIds(int[] movieIds)
            {
                Query
                    .Where(m => movieIds.Contains(m.Id))
                    .Include(m => m.MovieActors)
                    .Include(m => m.MovieGenres);
            }
        }

        public class All : Specification<Movie>
        {
            public All()
            {
                Query
                    .Include(m => m.MovieActors)
                        .ThenInclude(ma => ma.Actor)
                    .Include(m => m.MovieGenres)
                        .ThenInclude(mg => mg.Genre);
            }
        }
        public class FindActorByName : Specification<Actor>
        {
            public FindActorByName(string name, string surname)
            {
                Query.Where(actor => actor.Name == name && actor.Surname == surname);
            }
        }
        public class FindActorsByName : Specification<Actor>
        {
            public FindActorsByName(List<string> names, List<string> surnames)
            {
                Query.Where(actor => names.Contains(actor.Name) && surnames.Contains(actor.Surname));
            }
        }

        public class FindGenreByName : Specification<Genre>
        {
            public FindGenreByName(string name)
            {
                Query.Where(genre => genre.Name == name);
            }
        }

        public class GenresByIds : Specification<Genre>
        {
            public GenresByIds(int[] genreIds)
            {
                Query
                    .Where(m => genreIds.Contains(m.Id));
            }
        }

    }
    
}