using Microsoft.EntityFrameworkCore;
using RentalCompany.Models.CarModels;
using RentalCompany.Models.CustomerModel;
using RentalCompany.Models.DriverModel;

namespace RentalCompany.SqlServer
{
    public class ApplicationDbContext : DbContext
    {
        #region Ctor 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        #endregion

        #region realtion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Driver>()
                 .HasOne(c => c.Car)
                 .WithOne(d => d.driver).OnDelete(DeleteBehavior.Restrict).HasForeignKey<Car>(c => c.DriverId);
            modelBuilder.Entity<Customer>()
               .HasMany(c => c.RentedCars).WithOne(c => c.customer).OnDelete(DeleteBehavior.Restrict).HasForeignKey(c => c.CustomerId);


            modelBuilder.Entity<Car>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Customer>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Driver>().HasQueryFilter(e => !e.IsDeleted);



        }
        #endregion

        #region DbSetet
        public DbSet<Car> CarsSet { get; set; }
        public DbSet<Driver> DriversSet { get; set; }
        public DbSet<Customer> CustomersSet { get; set; }
        #endregion
    }
}
