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
                flight_attendant = _entry.flight_attendant,
                departure = _entry.departure,
                arrival = _entry.arrival,
                pay = _entry.pay,
                flight_hours = _entry.flight_hours
            };
            context.Logbook.Add(entry);
            context.SaveChanges();

            EmployeeModel pilot = context.Employees.FirstOrDefault(e => e.name == _entry.pilot);
            EmployeeModel copilot = context.Employees.FirstOrDefault(e => e.name == _entry.copilot);
            EmployeeModel flight_attendant = context.Employees.FirstOrDefault(e => e.name == _entry.flight_attendant);

            if (pilot != null && copilot != null && flight_attendant != null)
            {
                pilot.pay += _entry.pay / 2;
                _entry.pay -= (_entry.pay / 2);
                copilot.pay += _entry.pay / 2;
                _entry.pay -= (_entry.pay / 2);
                flight_attendant.pay += _entry.pay;
                pilot.hours += _entry.flight_hours;
                copilot.hours += _entry.flight_hours;
                context.SaveChanges();
            }

            return RedirectToAction("Index", "Logbook");
        }

        public IActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeModel employee, string inputName, string inputJob, IFormFile inputImage)
        {

            if (ModelState.IsValid)
            {
                if (inputImage != null && inputImage.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await inputImage.CopyToAsync(memoryStream);
                        employee.image_data = memoryStream.ToArray();
                        employee.image_mime = inputImage.ContentType;
                    }
                }
                var newEmployee = new EmployeeModel
                {
                    name = inputName,
                    job = inputJob,
                    experience = 1,
                    job_level = 1,
                    hours = 0,
                    pay = 0,
                    image_data = employee.image_data,
                    image_mime = employee.image_mime,
                };
                context.Employees.Add(newEmployee);
                await context.SaveChangesAsync();
                return View();
            }
            return View();
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
