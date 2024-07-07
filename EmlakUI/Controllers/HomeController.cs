using Microsoft.AspNetCore.Mvc;

namespace EmlakUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
