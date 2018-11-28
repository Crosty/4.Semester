using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Personkartotek.Models
{
    public partial class PersonkartotekDBHandIn32Context : DbContext
    {
        public PersonkartotekDBHandIn32Context()
        {
        }

        public PersonkartotekDBHandIn32Context(DbContextOptions<PersonkartotekDBHandIn32Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> AddressMigration { get; set; }
        public virtual DbSet<Email> EmailMigration { get; set; }
        public virtual DbSet<Person> PersonMigration { get; set; }
        public virtual DbSet<Zip> ZipMigration { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectsV13;Database=Personkartotek.DB.HandIn3.2;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.AddressId)
                    .ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.PersonId)
                    .HasName("fkIdx_138");

                entity.HasIndex(e => e.ZipId)
                    .HasName("fkIdx_100");

                entity.Property(e => e.HouseNumber).IsRequired();

                entity.Property(e => e.StreetName).IsRequired();

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_138");

                entity.HasOne(d => d.Zip)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.ZipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_100");
            });

            modelBuilder.Entity<Email>(entity =>
            {
                entity.HasIndex(e => e.PersonId)
                    .HasName("fkIdx_144");

                entity.Property(e => e.EmailAddress).IsRequired();

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Email)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_144");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.FirstName).IsRequired();

                entity.Property(e => e.LastName).IsRequired();

                entity.Property(e => e.MiddleName).IsRequired();

                entity.Property(e => e.PersonType).IsRequired();
            });

            modelBuilder.Entity<Zip>(entity =>
            {
                entity.Property(e => e.City).IsRequired();

                entity.Property(e => e.Country).IsRequired();

                entity.Property(e => e.ZipCode).IsRequired();
            });
        }
    }
}
