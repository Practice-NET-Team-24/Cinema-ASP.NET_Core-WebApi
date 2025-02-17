using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using System.Data;


namespace Application.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Movie, MovieDTO>()
             .ForMember(dest => dest.MovieActors, opt => opt.MapFrom(src =>
                 src.MovieActors.Select(ma => new MovieActorDTO
                 {
                     Id = ma.Id,
                     MovieId = ma.MovieId,
                     ActorId = ma.ActorId,
                     Role = ma.Role,
                     Actor = new ActorDTO
                        {
                            Id = ma.Actor.Id,
                            Name = ma.Actor.Name,
                            Surname = ma.Actor.Surname
                        }
                 })
             ))
             .ForMember(dest => dest.MovieGenres, opt => opt.MapFrom(src =>
                 src.MovieGenres.Select(mg => new MovieGenreDTO
                 {
                     Id = mg.Id,
                     MovieId = mg.MovieId,
                     GenreId = mg.GenreId,
                     Genre = new GenreDTO
                     {
                         Id = mg.Genre.Id,
                         Name = mg.Genre.Name
                     },


                 })
             ));

            // Map MovieDTO -> Movie
            CreateMap<MovieDTO, Movie>()
                .ForMember(dest => dest.MovieActors, opt => opt.MapFrom(src =>
                    src.MovieActors.Select(movieActorDTO => new MovieActor
                    {
                        Id = movieActorDTO.Id,
                        ActorId = movieActorDTO.ActorId,
                        Role = movieActorDTO.Role
                    })
                ))
                .ForMember(dest => dest.MovieGenres, opt => opt.MapFrom(src =>
                    src.MovieGenres.Select(genreDTO => new MovieGenre
                    {
                        Id = genreDTO.Id,
                        GenreId = genreDTO.GenreId
                    })
                ))
                .ForMember(dest => dest.Sessions, opt => opt.Ignore());

            CreateMap<ActorDTO, Actor>().ReverseMap();
            CreateMap<GenreDTO, Genre>().ReverseMap();


            CreateMap<SessionDTO, Session>().ReverseMap();
        }
    }
}
