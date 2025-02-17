using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Application.Specifications;
using static Application.Specifications.Movies;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Movie> _movieRepository;
        private readonly IRepository<Actor> _actorRepository;
        private readonly IRepository<Genre> _genreRepository;
        private readonly IRepository<MovieActor> _movieActorRepository;
        private readonly IRepository<MovieGenre> _movieGenreRepository;

        public MovieService(IMapper mapper,
            IRepository<Movie> movieRepository,
            IRepository<Actor> actorRepository,
            IRepository<Genre> genreRepository,
            IRepository<MovieActor> movieActorRepository,
            IRepository<MovieGenre> movieGenreRepository)
        {
            _mapper = mapper;
            _movieRepository = movieRepository;
            _actorRepository = actorRepository;
            _genreRepository = genreRepository;
            _movieActorRepository = movieActorRepository;
            _movieGenreRepository = movieGenreRepository;
        }

        public async Task<MovieDTO> CreateMovieAsync(MovieDTO movieDTO)
        {
            var movie = _mapper.Map<Movie>(movieDTO);

            // Handle Actors
            var existingActors = await GetExistingActors(movieDTO.MovieActors);
            movie.MovieActors.Clear();

            foreach (var movieActorDTO in movieDTO.MovieActors)
            {
                var actorData = movieActorDTO.Actor;
                var existingActor = existingActors.FirstOrDefault(a =>
                    a.Name == actorData.Name && a.Surname == actorData.Surname);

                var movieActor = new MovieActor
                {
                    Role = movieActorDTO.Role,
                    ActorId = existingActor?.Id ?? 0
                };

                if (existingActor == null)
                {
                    var newActor = new Actor { Name = actorData.Name, Surname = actorData.Surname };
                    var addedActor = await _actorRepository.InsertAsync(newActor);
                    movieActor.ActorId = addedActor.Id;
                }

                movie.MovieActors.Add(movieActor);
            }

            // Handle Genres
            movie.MovieGenres.Clear();
            foreach (var genreDTO in movieDTO.MovieGenres)
            {
                var existingGenre = await _genreRepository.GetFirstBySpecAsync(new FindGenreByName(genreDTO.Genre.Name));

                if (existingGenre == null)
                {
                    var newGenre = new Genre { Name = genreDTO.Genre.Name };
                    existingGenre = await _genreRepository.InsertAsync(newGenre);
                    await _genreRepository.SaveAsync();
                }

                var movieGenre = new MovieGenre
                {
                    GenreId = existingGenre.Id
                };

                movie.MovieGenres.Add(movieGenre);
            }

            var createdMovie = await _movieRepository.InsertAsync(movie);
            await _movieRepository.SaveAsync();

            return _mapper.Map<MovieDTO>(createdMovie);
        }

        public async Task<MovieDTO?> DeleteMovieAsync(int movieId)
        {
            var movie = await _movieRepository.DeleteAsync(movieId);
            await _movieRepository.SaveAsync();
            return movie == null ? null : _mapper.Map<MovieDTO>(movie);
        }

        public async Task<MovieDTO?> UpdateMovieAsync(MovieDTO movieDTO)
        {
            var movieSpec = new Movies.ById(movieDTO.Id);
            var updatingMovie = await _movieRepository.GetFirstBySpecAsync(movieSpec);

            if (updatingMovie == null) return null;

            // Map base properties
            _mapper.Map(movieDTO, updatingMovie);

            // Update actors
            await UpdateActors(updatingMovie, movieDTO.MovieActors);

            // Update genres
            await UpdateGenres(updatingMovie, movieDTO.MovieGenres);

            var updatedMovie = await _movieRepository.UpdateAsync(updatingMovie);
            await _movieRepository.SaveAsync();

            return _mapper.Map<MovieDTO>(updatedMovie);
        }

        private async Task UpdateActors(Movie movie, IEnumerable<MovieActorDTO> movieActorsDTO)
        {
            var existingActors = await GetExistingActors(movieActorsDTO);
            movie.MovieActors.Clear();

            foreach (var movieActorDTO in movieActorsDTO)
            {
                var actorData = movieActorDTO.Actor;
                var existingActor = existingActors.FirstOrDefault(a =>
                    a.Name == actorData.Name && a.Surname == actorData.Surname);

                var movieActor = new MovieActor
                {
                    MovieId = movie.Id,
                    ActorId = existingActor?.Id ?? 0,
                    Role = movieActorDTO.Role
                };

                if (existingActor == null)
                {
                    var newActor = new Actor { Name = actorData.Name, Surname = actorData.Surname };
                    var addedActor = await _actorRepository.InsertAsync(newActor);
                    movieActor.ActorId = addedActor.Id;
                }

                movie.MovieActors.Add(movieActor);
            }
        }

        private async Task<IEnumerable<Actor>> GetExistingActors(IEnumerable<MovieActorDTO> movieActorsDTO)
        {
            var actorNames = movieActorsDTO.Select(ma => ma.Actor.Name).Distinct().ToList();
            var actorSurnames = movieActorsDTO.Select(ma => ma.Actor.Surname).Distinct().ToList();

            var actorSpec = new FindActorsByName(actorNames, actorSurnames);
            return await _actorRepository.GetListBySpecAsync(actorSpec);
        }

        private async Task UpdateGenres(Movie movie, IEnumerable<MovieGenreDTO> movieGenresDTO)
        {
            // Clear existing movie genres
            movie.MovieGenres.Clear();

            foreach (var genreDTO in movieGenresDTO)
            {
                var existingGenre = await _genreRepository.GetFirstBySpecAsync(new FindGenreByName(genreDTO.Genre.Name));

                if (existingGenre == null)
                {
                    // Create a new genre if it doesn't exist
                    var newGenre = new Genre { Name = genreDTO.Genre.Name };
                    existingGenre = await _genreRepository.InsertAsync(newGenre);
                    await _genreRepository.SaveAsync();
                }

                // Add the movie genre relationship
                var movieGenre = new MovieGenre
                {
                    MovieId = movie.Id,
                    GenreId = existingGenre.Id
                };

                movie.MovieGenres.Add(movieGenre);
            }
        }

        public async Task<List<MovieDTO>> GetAllMoviesAsync()
        {
            var movieSpec = new Movies.All();
            var list = await _movieRepository.GetListBySpecAsync(movieSpec);
            return _mapper.Map<List<MovieDTO>>(list);
        }

        public async Task<List<MovieDTO>> GetMoviesAsync(int[] movieIds)
        {
            var movieSpec = new Movies.ByIds(movieIds);
            var list = await _movieRepository.GetListBySpecAsync(movieSpec);
            return _mapper.Map<List<MovieDTO>>(list);
        }

        public async Task<MovieDTO?> GetMovieAsync(int movieId)
        {
            var movieSpec = new Movies.ById(movieId);
            var movie = await _movieRepository.GetFirstBySpecAsync(movieSpec);
            return movie == null ? null : _mapper.Map<MovieDTO>(movie);
        }
    }
}
