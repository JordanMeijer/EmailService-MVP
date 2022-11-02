using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EmailService.Models
{
    public partial class DIEmailContext : DbContext
    {
        public DIEmailContext()
        {}

        public DIEmailContext(DbContextOptions<DIEmailContext> options)
            : base(options)
        {}

        public virtual DbSet<EmailLog> EmailLogs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmailLog>(entity =>
            {
                entity.HasComment("For storing logs of email requests");

                entity.Property(e => e.EmailLogId).ValueGeneratedOnAdd();

                entity.Property(e => e.EmailTo)
                    .HasMaxLength(254)
                    .IsFixedLength();

                entity.Property(e => e.Subject)
                    .HasMaxLength(230)
                    .IsFixedLength();

                entity.Property(e => e.Time);
                    
            });
           
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
