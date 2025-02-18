using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Seed Actors
            modelBuilder.Entity<Actor>().HasData(
                new Actor { Id = 1, Name = "Robert", Surname = "Downey Jr." },
                new Actor { Id = 2, Name = "Chris", Surname = "Evans" },
                new Actor { Id = 3, Name = "Scarlett", Surname = "Johansson" }
            );

            // Seed Genres
            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Action" },
                new Genre { Id = 2, Name = "Drama" },
                new Genre { Id = 3, Name = "Sci-Fi" }
            );

            // Seed Movies
            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    Id = 1,
                    Name = "Avengers: Endgame",
                    Description = "Superhero movie",
                    ImageURL = "https://example.com/image1.jpg",
                    TrailerURL = "https://example.com/trailer1.mp4",
                    AgeRestriction = 13,
                    Duration = 181, // 181 minutes
                    Rating = 9.0f
                },
                new Movie
                {
                    Id = 2,
                    Name = "Inception",
                    Description = "Sci-Fi thriller",
                    ImageURL = "https://example.com/image2.jpg",
                    TrailerURL = "https://example.com/trailer2.mp4",
                    AgeRestriction = 16,
                    Duration = 148, // 148 minutes
                    Rating = 8.8f
                }
            );

            // Seed MovieGenre relationships
            modelBuilder.Entity<MovieGenre>().HasData(
                new MovieGenre { Id = 1, MovieId = 1, GenreId = 1 },
                new MovieGenre { Id = 2, MovieId = 1, GenreId = 3 },
                new MovieGenre { Id = 3, MovieId = 2, GenreId = 3 }
            );

            // Seed MovieActor relationships
            modelBuilder.Entity<MovieActor>().HasData(
                new MovieActor { Id = 1, MovieId = 1, ActorId = 1, Role = "Tony Stark" },
                new MovieActor { Id = 2, MovieId = 1, ActorId = 2, Role = "Captain America" },
                new MovieActor { Id = 3, MovieId = 2, ActorId = 3, Role = "Ariadne" }
            );

            // Seed Halls
            modelBuilder.Entity<Hall>().HasData(
                new Hall { Id = 1, Name = "IMAX" },
                new Hall { Id = 2, Name = "Standard" }
            );

            // Seed Places
            modelBuilder.Entity<Place>().HasData(
                new Place { Id = 1, HallId = 1, RowNumber = 1, SeatNumber = 1 },
                new Place { Id = 2, HallId = 1, RowNumber = 1, SeatNumber = 2 },
                new Place { Id = 3, HallId = 2, RowNumber = 1, SeatNumber = 1 }
            );

            // Seed Sessions (DateTimeEnd = DateTimeStart + Movie Duration)
            modelBuilder.Entity<Session>().HasData(
                new Session
                {
                    Id = 1,
                    MovieId = 1, // Avengers: Endgame (181 min)
                    HallId = 1,
                    DateTimeStart = new DateTime(2025, 02, 15, 18, 00, 00),
                    DateTimeEnd = new DateTime(2025, 02, 15, 18, 00, 00).AddMinutes(181),
                    Price = 15.99f,
                    ReservedPlaces = 0
                },
                new Session
                {
                    Id = 2,
                    MovieId = 2, // Inception (148 min)
                    HallId = 2,
                    DateTimeStart = new DateTime(2025, 02, 16, 15, 30, 00),
                    DateTimeEnd = new DateTime(2025, 02, 16, 15, 30, 00).AddMinutes(148),
                    Price = 12.99f,
                    ReservedPlaces = 5
                },
                new Session
                {
                    Id = 3,
                    MovieId = 1, // Avengers: Endgame (181 min)
                    HallId = 2,
                    DateTimeStart = new DateTime(2025, 02, 17, 20, 00, 00),
                    DateTimeEnd = new DateTime(2025, 02, 17, 20, 00, 00).AddMinutes(181),
                    Price = 16.50f,
                    ReservedPlaces = 10
                },
                new Session
                {
                    Id = 4,
                    MovieId = 2, // Inception (148 min)
                    HallId = 1,
                    DateTimeStart = new DateTime(2025, 02, 18, 17, 45, 00),
                    DateTimeEnd = new DateTime(2025, 02, 18, 17, 45, 00).AddMinutes(148),
                    Price = 14.50f,
                    ReservedPlaces = 2
                }
            );
        }
    }
}
