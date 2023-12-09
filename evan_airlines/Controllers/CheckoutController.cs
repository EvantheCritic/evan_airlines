using evan_airlines.Interfaces;
using evan_airlines.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using System;

namespace evan_airlines.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly AppDbContext context;
        public CheckoutController(AppDbContext _context)
        {
            context = _context;
        }

        public IActionResult Index(string user)
        {
            var cart = context.Checkout
                .Where(entry => entry.User == user)
                .ToList();
            return View(cart);
        }

        public string generateConfirmationCode()
        {
            string confirmation = "";
            for (int i = 0; i < 5; i++)
            {
                Random random = new Random();
                char c = (char)random.Next('A', 'Z' + 1);
                confirmation += c;
            }
            return confirmation;
        }

        [HttpPost]
        public IActionResult ThankYou(CheckoutModel checkout, string firstName, string lastName)
        {

            string confirmationCode = generateConfirmationCode();
            var entriesToRemove = context.Checkout.Where(entry => entry.User == checkout.User).ToList();
            if (firstName == null || lastName == null || entriesToRemove.Count == 0)
            {
                throw new ApplicationException("XDDCC");
            }
            foreach (var entry in entriesToRemove)
            {
                ConfirmationModel confirmation = new ConfirmationModel
                {
                    ConfirmationCode = confirmationCode,
                    FirstName = firstName,
                    LastName = lastName,
                    Route = entry.Departure + " - " + entry.Arrival,
                    Number = entry.Number
                };
                context.Confirmation.Add(confirmation);
            }

            context.Checkout.RemoveRange(entriesToRemove);
            context.SaveChanges();

            // Now, retrieve confirmations for the specific confirmationCode
            var confirmations = context.Confirmation
                .Where(entry => entry.ConfirmationCode == confirmationCode)
                .ToList();

            return View(confirmations);
        }
    }
}
