using Microsoft.AspNetCore.Mvc;

namespace TUSO.Portal.Controllers
{
    public class CountryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
