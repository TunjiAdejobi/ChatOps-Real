using ChatOps.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ChatOps.Data.DataContext
{
    public class ChatOpsDbContext : DbContext
    {
        public ChatOpsDbContext(DbContextOptions<ChatOpsDbContext> options) 
            : base(options)
        {

        }

        public ChatOpsDbContext()
        {

        }
               
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<Occupation> Occupations { get; set; }               
        //public virtual DbSet<ChatMessage> ChatMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entity relationships or additional constraints if needed
            modelBuilder.Entity<UserProfile>()
                .HasOne(u => u.Occupation)
                .WithMany()
                .HasForeignKey(u => u.OccupationId);
            modelBuilder.Entity<UserProfile>()
                .Property(u => u.IsActive)
                .HasDefaultValue(false);
            modelBuilder.Entity<UserProfile>()
                .HasIndex(u => u.Username)
                .IsUnique();
            modelBuilder.Entity<UserProfile>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<UserProfile>()
                .Property(u => u.Username)
                .IsRequired();

            modelBuilder.Entity<Occupation>()
                 .HasKey(d => d.Id);
            modelBuilder.Entity<Occupation>()
              .HasIndex(u => u.Industry)
              .IsUnique();
        }
    }
}
