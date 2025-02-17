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
                    Duration = 181,
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
                    Duration = 148,
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
                new MovieActor { Id = 2, MovieId = 1, ActorId = 2, Role = "Captain Amerika"  },
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

            // Seed Sessions
            modelBuilder.Entity<Session>().HasData(
                new Session
                {
                    Id = 1,
                    MovieId = 1,
                    HallId = 1,
                    DateTimeStart = new DateTime(2025, 02, 15, 18, 00, 00),  // Static value
                    DateTimeEnd = new DateTime(2025, 02, 15, 21, 00, 00),    // Static value
                    Price = 15.99f,
                    ReservedPlaces = 0
                }
            );

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Admin", Email = "admin@example.com", Role = UserRole.Admin },
                new User { Id = 2, Name = "User1", Email = "user1@example.com", Role = UserRole.Client }
            );

            // Seed Tickets
            modelBuilder.Entity<Ticket>().HasData(
                new Ticket
                {
                    Id = 1,
                    PlaceId = 1,
                    SessionId = 1,
                    UserId = 2,
                    CreatedAt = new DateTime(2025, 02, 10, 12, 00, 00)  // Static value
                }
            );
        }
    }
}
