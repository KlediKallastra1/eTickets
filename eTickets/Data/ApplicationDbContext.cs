﻿using eTickets.Entities;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor_Movie>().HasKey(am => new
            {
                am.ActorId,
                am.MovieId
            });

            modelBuilder.Entity<Actor_Movie>().HasOne(am => am.Movie).WithMany(am => am.Actors_Movies).HasForeignKey(am => am.MovieId);
            modelBuilder.Entity<Actor_Movie>().HasOne(am => am.Actor).WithMany(am => am.Actors_Movies).HasForeignKey(am => am.ActorId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Actor>? Actors { get; set; }
        public DbSet<Movie>? Movies { get; set; }
        public DbSet<Actor_Movie>? Actors_Movies { get; set; }
        public DbSet<Cinema>? Cinema { get; set; }
        public DbSet<Producer>? Producers { get; set; }
    }
}