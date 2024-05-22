using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
