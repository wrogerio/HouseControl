using HouseControl.Infra.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace HouseControl.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILancamentoRepository lancamentoRepository;

        public HomeController(ILancamentoRepository lancamentoRepository)
        {
            this.lancamentoRepository = lancamentoRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["title"] = "Dashboard";
            return View();
        }

        [HttpGet]
        public JsonResult GetResumoMensal(string MesAno)
        {
            var mes = MesAno.Split('-')[1];
            var ano = MesAno.Split('-')[0];

            dynamic ret = new List<ExpandoObject>();

            dynamic _item = new ExpandoObject();
            _item.mes = mes;
            _item.ano = ano;

            var lancamentos = lancamentoRepository.GetAllAsync().Result.Where(x => x.DtLancamento.Month == int.Parse(mes) && x.DtLancamento.Year == int.Parse(ano)).ToList().OrderBy(x => x.Categoria.Nome);
            var categorias = lancamentos.Select(x => x.Categoria?.Nome).Distinct().ToList();

            foreach (var categ in categorias)
            {
                var _lanc = lancamentos.Where(x => x.Categoria.Nome == categ).ToList();
                
                dynamic _obj = new ExpandoObject();
                _obj.categoria = categ;
                _obj.valor = _lanc.Sum(x => x.Valor);
                ret.Add(_obj);
            }

            _item.total = lancamentos.Sum(x => x.Valor);
            _item.dados = ret;
            return Json(_item);
        }
    }
}