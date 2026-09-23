using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BUA_project.Models
{
    public class Entity : IdentityDbContext<ApplicationUser>
    {
        public Entity() : base()
        {
        }

        public Entity(DbContextOptions<Entity> options) : base(options)
        {

        }
        // User
        public DbSet<User> Users { get; set; }

        // Fleet
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleSpecification> VehicleSpecifications { get; set; }

        public DbSet<FuelPrice> FuelPrices { get; set; }

        public DbSet<Driver> Drivers { get; set; }

        // Reservation
        public DbSet<Reservation> Reservations { get; set; }

        // Estimates
        public DbSet<RouteEstimate> RouteEstimates { get; set; }
        public DbSet<FuelEstimate> FuelEstimates { get; set; }

        // Trip & Tracking
        public DbSet<Trip> Trips { get; set; }
        public DbSet<LocationPing> LocationPings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Console.WriteLine("=== EF MODEL ENTITIES ===");

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                Console.WriteLine(entity.ClrType.FullName);
            }

            Console.WriteLine("=========================");

            modelBuilder.Entity<ApplicationUser>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<ApplicationUser>(u => u.BusinessUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationUser>()
                .HasIndex(u => u.BusinessUserId)
                .IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //option(dbms)
            optionsBuilder.UseSqlServer("Server=.;Database=BUA_ProjectDB;Trusted_Connection=True;TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
