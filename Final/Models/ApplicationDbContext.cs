using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Final.Models
{
    public partial class ApplicationDbContext : DbContext
    {    

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Story> Stories { get; set; } = null!;
        public virtual DbSet<Word> Words { get; set; } = null!;

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Story>(entity =>
            {
                entity.HasIndex(e => e.Letter, "IX_Stories_Letter");

                entity.Property(e => e.Language)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('Bengali')");

                entity.Property(e => e.Letter)
                    .HasMaxLength(1)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Word>(entity =>
            {
                entity.HasIndex(e => e.Letter, "IX_Words_Letter");

                entity.Property(e => e.Definition).HasMaxLength(500);

                entity.Property(e => e.Letter)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.Text).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
