using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SportsTest.Models
{
    public partial class SportsTestContext : DbContext
    {
        public SportsTestContext()
        {
        }

        public SportsTestContext(DbContextOptions<SportsTestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MemberTestMapping> MemberTestMapping { get; set; }
        public virtual DbSet<Memders> Memders { get; set; }
        public virtual DbSet<TestDetails> TestDetails { get; set; }
        public virtual DbSet<Tests> Tests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectsV13;Initial Catalog=SportsTest;Integrated Security=True;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<MemberTestMapping>(entity =>
            {
                entity.HasKey(e => e.MemberTestId)
                    .HasName("PK__MemberTe__1B635377C018AB5B");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Memders>(entity =>
            {
                entity.HasKey(e => e.MemberId)
                    .HasName("PK__Memders__0CF04B189AABF253");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MemberName)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<TestDetails>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Date).HasColumnType("date");
            });

            modelBuilder.Entity<Tests>(entity =>
            {
                entity.HasKey(e => e.TestId)
                    .HasName("PK__Tests__3214EC0790E97362");

                entity.Property(e => e.SportsName).HasMaxLength(50);
            });
        }
    }
}
