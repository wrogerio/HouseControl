using Microsoft.AspNetCore.Mvc;

namespace HouseControl.UI.Controllers
{
    public class LancamentosController : Controller
    {
        public IActionResult Index()
        {
            ViewData["title"] = "Lançamentos";
            return View();
        }
    }
}
