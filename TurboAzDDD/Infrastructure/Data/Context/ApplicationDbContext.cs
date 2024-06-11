using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BodyType> BodyTypes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Domain.Entities.DriveType> DriveTypes { get; set; }
        public DbSet<FuelType> FuelType { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Market> Markets { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<NumberOfOwner> NumberOfOwners { get; set; }
        public DbSet<NumberOfSeat> NumberOfSeats { get; set; }
        public DbSet<Salon> Salons { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagVehicle> TagVehicles { get; set; }
        public DbSet<Transmission> Transmissions { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Contact> Contacts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Vehicle>()
            //    .Property(b => b.CreatedDate)
            //    .HasDefaultValue(DateTime.UtcNow);

            //modelBuilder.Entity<Vehicle>()
            //   .Property(b => b.IsDeleted)
            //   .HasDefaultValue(0);

            //modelBuilder.Entity<Brand>()
            //  .Property(b => b.CreatedDate)
            //  .HasDefaultValue(DateTime.UtcNow);

            //modelBuilder.Entity<Brand>()
            //   .Property(b => b.IsDeleted)
            //   .HasDefaultValue(0);
        }


    }
}

