using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using evan_airlines.Properties.Models;

namespace evan_airlines.wwwroot
{
    public class AppDbContext : IdentityDbContext<UserModel>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<LogbookModel> Logbook { get; set; }
    }
}
