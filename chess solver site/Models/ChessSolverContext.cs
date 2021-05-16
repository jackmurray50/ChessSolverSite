using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace chess_solver_site.Models
{
    public class ChessSolverContext : DbContext
    {
        public ChessSolverContext()
        {

        }

        public ChessSolverContext(DbContextOptions<ChessSolverContext> options) : base(options)
        {

        }

        public virtual DbSet<Boards> Boards { get; set; }
        public virtual DbSet<Accounts> Accounts { get; set; }
    
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectsV13;Initial Catalog=ChessSolverDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                optionsBuilder.UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Boards>(entity =>
            {
                entity.Property(e => e.BoardState)
                    .HasMaxLength(64)
                    .IsUnicode(true);
                entity.Property(e => e.Turn)
                    .HasMaxLength(5);
            });

            modelBuilder.Entity<BoardsRelationships>(entity =>
            {

            });

            var converter = new ValueConverter<int, long>(
                i => Convert.ToInt64(i),
                l => Convert.ToInt32(l));
            modelBuilder.Entity<Accounts>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(32)
                    .IsUnicode(false);
                entity.Property(e => e.Password)
                    .HasMaxLength(64)
                    .IsUnicode(false);
                entity.Property(e => e.Progress)
                    .HasConversion(converter);
            });
        }
    
    
    }
}
