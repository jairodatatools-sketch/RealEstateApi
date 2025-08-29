using Microsoft.EntityFrameworkCore;

using RealEstateApi.Domain.Entities;

namespace RealEstateApi.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Property>().ToTable("Property");
            modelBuilder.Entity<PropertyImage>().ToTable("PropertyImage");
            modelBuilder.Entity<Owner>().ToTable("Owner");


            modelBuilder.Entity<Property>().HasKey(p => p.IdProperty);

            modelBuilder.Entity<PropertyImage>().HasKey(pi => pi.IdPropertyImage);
            modelBuilder.Entity<PropertyImage>()
                .HasOne(pi => pi.Property)
                .WithMany(p => p.Images)
                .HasForeignKey(pi => pi.IdProperty);

            modelBuilder.Entity<Owner>().HasKey(o => o.IdOwner);
            modelBuilder.Entity<Property>()
                .HasOne(p => p.Owner)
                .WithMany(o => o.Properties)
                .HasForeignKey(p => p.IdOwner);

            modelBuilder.Entity<PropertyTrace>().HasKey(pt => pt.IdPropertyTrace);
            modelBuilder.Entity<PropertyTrace>().ToTable("PropertyTrace");
            modelBuilder.Entity<PropertyTrace>()
                .HasOne(pt => pt.Property)
                .WithMany(p => p.Traces)
                .HasForeignKey(pt => pt.IdProperty);



        }
    }

}
