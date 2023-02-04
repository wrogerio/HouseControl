using Microsoft.AspNetCore.Mvc;

namespace HouseControl.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["title"] = "Dashboard";
            return View();
        }
    }
}