using Microsoft.EntityFrameworkCore;
using evan_airlines.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace evan_airlines
{
    public class AppDbContext : IdentityDbContext<UserModel>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<LogbookModel> Logbook { get; set; }
        public DbSet<FlightModel> Flights { get; set; }
        public DbSet<CheckoutModel> Checkout { get; set; }
        public DbSet<ConfirmationModel> Confirmation { get; set; }
    }
}
