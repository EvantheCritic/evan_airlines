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
    }
}
