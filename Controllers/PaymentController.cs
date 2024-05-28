﻿using EcommerceApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;

namespace EcommerceApp.Controllers
{
    public class PaymentController : Controller
    {
        private readonly MvcContext _context;
        public PaymentController(MvcContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Items.ToListAsync());
        }
        [HttpPost]
        public ActionResult CreateCheckout()
        {
            var domain = "https://localhost:7095";
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    // Provide the exact Price ID (for example, pr_1234) of the product you want to sell
                    Price = "price_1PJK48Ls2ndSVWb2ISIdMmqd",
                    Quantity = 1,
                  },
                },
                Mode = "payment",
                SuccessUrl = domain + "/Payment/Success",
                CancelUrl = domain + "/Payment/Cancel",
            };
            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }
        public IActionResult Success()
        {
            return View();
        }
        public IActionResult Cancel()
        {
            return View();
        }
    }
}
