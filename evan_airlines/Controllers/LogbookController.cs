using evan_airlines.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace evan_airlines.Controllers
{
    public class LogbookController : Controller
    {
        private readonly AppDbContext context;
        public LogbookController(AppDbContext _context)
        {
            context = _context;
        }

        public IActionResult Index()
        {
            var flights = context.Logbook.ToList();
            return View(flights);
        }

        public IActionResult Flown(string name)
        {
            var filteredFlights = context.Logbook.Where(flight => flight.pilot == name || flight.copilot == name).ToList();                
            return View(filteredFlights); // Pass the filtered flights to the view
        }
    }
}
