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
                checkin_asc = _entry.checkin_asc,
                ground1 = _entry.ground1,
                ground2 = _entry.ground2,
                gate_asc = _entry.gate_asc,
                departure = _entry.departure,
                arrival = _entry.arrival,
                flight_hours = _entry.flight_hours,
                flight_minutes = _entry.flight_minutes,
                pay = _entry.pay,
                
            };
            context.Logbook.Add(entry);
            context.SaveChanges();

            var Employees = context.Employees.ToList();
            foreach (var emplyoee in Employees)
            {
                double hours = (double)entry.flight_hours;
                double minutes = (double)(entry.flight_minutes / 60);
                if (emplyoee.name == "Evan")
                {
                    emplyoee.pay += (entry.pay / 2);
                    if (emplyoee.name == entry.pilot)
                    {
                        emplyoee.pay += (entry.pay / 8);
                        emplyoee.hours += hours;
                        emplyoee.hours += minutes;
                    }
                }
                else if (emplyoee.name == entry.pilot)
                {
                    emplyoee.pay += (entry.pay / 8);
                    emplyoee.hours += hours;
                    emplyoee.hours += minutes;
                }
                else if (emplyoee.name == entry.copilot)
                {
                    emplyoee.pay += (entry.pay / 16);
                    emplyoee.hours += hours;
                    emplyoee.hours += minutes;
                }
                else if (emplyoee.name == entry.fa_1 || emplyoee.name == entry.fa_2 || emplyoee.name == entry.checkin_asc
                    || emplyoee.name == entry.ground1 || emplyoee.name == entry.ground2 || emplyoee.name == entry.gate_asc
                    || emplyoee.job == "Schedule Manager") emplyoee.pay += (entry.pay / 32);
                else if (emplyoee.job == "Maintenance") emplyoee.pay += (entry.pay / 36);
                else continue;
            }
            context.SaveChanges();
            return RedirectToAction("Index", "Logbook");
        }

        [HttpGet]
        public IActionResult Pay(int id)
        {
            EmployeeModel employee = context.Employees.FirstOrDefault(e => e.id == id);
            return View(employee);
        }

        [HttpPost]
        public IActionResult Pay(int id, double inputPay, double inputHours)
        {
            var employee = context.Employees.FirstOrDefault(e => e.id == id);
            if (employee != null)
            {
                employee.pay += inputPay;
                employee.hours += inputHours;
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
        public IActionResult Job(int id)
        {
            EmployeeModel employee = context.Employees.FirstOrDefault(e => e.id == id);
            return View(employee);
        }

        public IActionResult setJob(int id, string newJob)
        {
            var employee = context.Employees.FirstOrDefault(e => e.id == id);
            employee.job = newJob;
            context.SaveChanges();
            return RedirectToAction("Detail", "Employees", new { id = employee.id });
        }

        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            context.Employees.Remove(employee);
            await context.SaveChangesAsync();
            return RedirectToAction("Index", "Employees"); // Redirect to the employee list page (adjust as needed)
        }
    }
}
