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

            var lancamentos = lancamentoRepository.GetAllAsync().Result.Where(x => x.DtLancamento.Month == int.Parse(mes) && x.DtLancamento.Year == int.Parse(ano)).ToList().OrderBy(x => x.Categoria.Nome);
            var categorias = lancamentos.Select(x => x.Categoria?.Nome).Distinct().ToList();

            dynamic _item = new ExpandoObject();
            _item.mes = mes;
            _item.ano = ano;
            _item.total = lancamentos.Sum(x => x.Valor);

            foreach (var categ in categorias)
            {
                var _lanc = lancamentos.Where(x => x.Categoria.Nome == categ).ToList();
                
                dynamic _obj = new ExpandoObject();
                _obj.categoria = categ;
                _obj.valor = _lanc.Sum(x => x.Valor);
                _obj.porcentagem = _obj.valor / _item.total * 100;
                ret.Add(_obj);
            }

            _item.dados = ret;
            return Json(_item);
        }
    }
}