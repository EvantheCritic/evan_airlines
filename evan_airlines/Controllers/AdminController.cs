using evan_airlines.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace evan_airlines.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AppDbContext context;

        public AdminController(AppDbContext _context) 
        {
            context = _context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddEntry()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitEntry(LogbookModel _entry)
        {
            LogbookModel entry = new LogbookModel
            {
                pilot = _entry.pilot,
                copilot = _entry.copilot,
                fa_1 = _entry.fa_1,
                fa_2 = _entry.fa_2,
                departure = _entry.departure,
                arrival = _entry.arrival,
                hours = _entry.hours,
                minutes = _entry.minutes,
                pay = _entry.pay,
                
            };
            context.Logbook.Add(entry);
            context.SaveChanges();
            double time = entry.hours;
            double dec = entry.minutes / 60;
            time += dec;
            var Employees = context.Employees.ToList();
            foreach (var employee in Employees)
            {
                if (employee.name == entry.pilot || employee.name == entry.copilot)
                {
                    employee.pilot_hours += time;
                }
                else if (employee.name == entry.fa_1 || employee.name == entry.fa_2)
                {
                    employee.fa_hours += time;
                }
            }
            context.SaveChanges();
            return RedirectToAction("Index", "Logbook");
        }

        [HttpGet]
        public IActionResult Addhours(int id)
        {
            EmployeeModel employee = context.Employees.FirstOrDefault(e => e.id == id);
            return View(employee);
        }

        [HttpPost]
        public IActionResult Addhours(int id, double inputPay, double inputHours)
        {
            var employee = context.Employees.FirstOrDefault(e => e.id == id);
            if (employee != null)
            {
                employee.pilot_hours += inputHours;
                context.SaveChanges();
                return RedirectToAction("Detail", "Employees", new { id = employee.id });
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        public IActionResult LevelUp(int id)
        {
            EmployeeModel employee = context.Employees.FirstOrDefault(e => e.id == id);
            return View(employee);
        }

        [HttpPost]
        public IActionResult LevelUp(int id, int inputExperience, int inputJobLevel)
        {
            var employee = context.Employees.FirstOrDefault(e => e.id == id);
            if (employee != null)
            {
                employee.experience = inputExperience;
                employee.job_level = inputJobLevel;
                context.SaveChanges();
                return View(employee);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
