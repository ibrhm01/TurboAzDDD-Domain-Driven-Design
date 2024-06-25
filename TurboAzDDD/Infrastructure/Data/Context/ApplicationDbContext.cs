using Domain.Entities;
using Domain.ENUMs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
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
        public DbSet<Salon> Salons { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagVehicle> TagVehicles { get; set; }
        public DbSet<Transmission> Transmissions { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Contact> Contacts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            List<IdentityRole> roles = new()
            {
                new IdentityRole
                {
                    Name = Role.Member.ToString(),
                    NormalizedName = Role.Member.ToString().ToUpper()

                },

                new IdentityRole
                {
                    Name = Role.Admin.ToString(),
                    NormalizedName = Role.Admin.ToString().ToUpper()

                },

                new IdentityRole
                {
                    Name = Role.SuperAdmin.ToString(),
                    NormalizedName = Role.SuperAdmin.ToString().ToUpper()

                }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);

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

