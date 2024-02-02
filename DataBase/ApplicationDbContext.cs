using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Serie> series { get; set; }
        public DbSet<Genre> genres { get; set; }
        public DbSet<Producer> producers { get; set; }
        public DbSet<SeriesGenre> seriesGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            #region tables
            modelBuilder.Entity<Serie>().ToTable("Series");
            modelBuilder.Entity<Genre>().ToTable("Genres");
            modelBuilder.Entity<Producer>().ToTable("Producers");
            modelBuilder.Entity<SeriesGenre>().ToTable("SeriesGenres");
            #endregion

            #region primaryKey
            modelBuilder.Entity<Serie>().HasKey(s => s.Id);
            modelBuilder.Entity<Genre>().HasKey(g => g.Id );
            modelBuilder.Entity<Producer>().HasKey(p => p.Id );

            modelBuilder.Entity<SeriesGenre>().HasKey(s => new {s.SerieId, s.GenreId});
            #endregion

            #region relationship
            modelBuilder.Entity<Producer>()
                .HasMany<Serie>(s => s.SerieProducerList)
                .WithOne(s => s.Producer)
                .HasForeignKey(s => s.ProducerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SeriesGenre>()
                .HasOne(sg => sg.Serie)
                .WithMany(s => s.SeriesGenresList)
                .HasForeignKey(s => s.SerieId)
                .OnDelete(DeleteBehavior.Cascade);
                

            modelBuilder.Entity<SeriesGenre>()
                .HasOne(sg => sg.Genre)
                .WithMany(g => g.GenresSeriesList)
                .HasForeignKey(g => g.GenreId)
                .OnDelete(DeleteBehavior.Cascade);


            #endregion

            #region producer
            modelBuilder.Entity<Producer>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);
            #endregion

            #region series
            modelBuilder.Entity<Serie>()
                .Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);
            #endregion
        }

    }
}
