using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Abstractions;

namespace HouseControl.UI.Controllers
{
    [Route("Error/{statuscode}")]
    public class ErrorController : Controller
    {
        public IActionResult Index(int statuscode)
        {
            switch (statuscode)
            {
                case 404:
                    ViewBag.Erro = "Página não encontrada";
                    break;
                case 501:
                    ViewBag.Erro = "Erro interno do servidor";
                    break;
                default:
                    ViewBag.Erro = "Erro não identificado";
                    break;
            }
            return View("Index");
        }
    }
}
