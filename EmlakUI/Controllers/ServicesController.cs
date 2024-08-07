using Microsoft.AspNetCore.Mvc;

namespace EmlakUI.Controllers
{
    public class ServicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
