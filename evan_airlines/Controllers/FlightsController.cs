﻿using evan_airlines.Interfaces;
using evan_airlines.Models;
using Microsoft.AspNetCore.Mvc;

namespace evan_airlines.Controllers
{
    public class FlightsController : Controller
    {
        private readonly AppDbContext context;
        private readonly FlightService flightService;

        public FlightsController(AppDbContext _context, IWebHostEnvironment _webHostEnvironment, FlightService _flightService)
        {
            context = _context;
            flightService = _flightService;
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
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(FlightModel flight, string user)
        {
            CheckoutModel entry = new CheckoutModel
            {
                User = user,
                Departure = flight.departure,
                Arrival = flight.arrival,
                Number = flight.number,
                Cost = flight.cost,
            };
            try
            {
                context.Checkout.Add(entry);
                context.SaveChanges();
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
