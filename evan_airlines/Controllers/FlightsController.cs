using evan_airlines.Interfaces;
using evan_airlines.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace evan_airlines.Controllers
{
    public class FlightsController : Controller
    {
        private readonly AppDbContext context;
        private readonly FlightService flightService;
        private readonly ICheckoutService checkoutService;

        public FlightsController(AppDbContext _context, IWebHostEnvironment _webHostEnvironment, FlightService _flightService, ICheckoutService _checkoutService)
        {
            context = _context;
            flightService = _flightService;
            checkoutService = _checkoutService;
        }
        public IActionResult Index()
        {
            var flights = flightService.GetAllFlights();
            return View(flights);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string departure, string arrival)
        {
            // Filter flights based on departure and arrival
            var filteredFlights = flightService.SearchFlights(departure, arrival);

            // Return a partial view with the filtered flights
            return View(filteredFlights);
        }

        [HttpPost]
        public IActionResult AddToCart(FlightModel flight)
        {
            try
            {
                checkoutService.AddFlightToCart(flight);
                ViewBag.AddToCartMessage = "Flight added to cart!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.AddToCartMessage = "Error adding flight to cart";
                return RedirectToAction("Index");
            }
        }
    }
}
