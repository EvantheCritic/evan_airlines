using evan_airlines.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace evan_airlines.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly AppDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public EmployeesController(AppDbContext _context, IWebHostEnvironment _webHostEnvironment) 
        {
            context = _context;
            webHostEnvironment = _webHostEnvironment;
        }
        public IActionResult Index()
        {
            var employees = context.Employees.ToList();
            return View(employees);
        }

        public IActionResult Detail(int id)
        {
            EmployeeModel employee = context.Employees.FirstOrDefault(e => e.id == id);
            return View(employee);
        }
    }
}
