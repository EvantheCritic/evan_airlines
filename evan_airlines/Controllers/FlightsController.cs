using Microsoft.AspNetCore.Mvc;

namespace evan_airlines.Controllers
{
    public class FlightsController : Controller
    {
        private readonly AppDbContext context;

        public FlightsController(AppDbContext _context, IWebHostEnvironment _webHostEnvironment)
        {
            context = _context;
        }
        public IActionResult Index()
        {
            var flights = context.Flights.ToList();
            return View(flights);
        }
    }
}
