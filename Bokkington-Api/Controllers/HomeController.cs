using Microsoft.AspNetCore.Mvc;

namespace Bokkington_Api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
