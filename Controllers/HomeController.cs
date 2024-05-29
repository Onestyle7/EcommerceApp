using EcommerceApp.Data;
using EcommerceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;
using System.Diagnostics;

namespace EcommerceApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MvcContext _context;
        

        public HomeController(ILogger<HomeController> logger, MvcContext context)
        {
            _logger = logger;
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.Items.ToListAsync());
        }

        [HttpPost]
        public ActionResult CreateCheckout([Bind("Id, Name, ImageUrl, PriceId")] Item item)
        {
            var domain = "https://localhost:7095";
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    // Provide the exact Price ID (for example, pr_1234) of the product you want to sell
                    Price = $"{item.PriceId}",
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
            public IActionResult Privacy()
            {
                return View();
            }

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    }

