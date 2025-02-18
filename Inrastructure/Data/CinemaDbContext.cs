using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class CinemaDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public CinemaDbContext() { }

        public CinemaDbContext(DbContextOptions options) : base(options) { }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=188.166.13.38,1433;Database=CinemaDb;User Id=sa;Password=Asd123asd.;Trusted_Connection=False;MultipleActiveResultSets=true;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ticket - Place relationship
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Place)
                .WithMany()
                .HasForeignKey(t => t.PlaceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ticket - Session relationship (No cascade)
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Session)
                .WithMany(s => s.Tickets)
                .HasForeignKey(t => t.SessionId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            // Ticket - User relationship (No cascade)
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete
            modelBuilder.Entity<MovieActor>()
              .HasOne(ma => ma.Actor)
              .WithMany(a => a.MovieActors)
              .HasForeignKey(ma => ma.ActorId)
              .OnDelete(DeleteBehavior.Cascade);
            
            
            ModelBuilderExtensions.Seed(modelBuilder);

        }
        

        public virtual DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public DbSet<Hall> Halls { get; set; }
        public DbSet<Place> Places { get; set; }

        public DbSet<Session> Sessions { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
