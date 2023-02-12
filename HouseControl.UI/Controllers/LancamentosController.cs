using HouseControl.Domain.Entities;
using HouseControl.Infra.Interfaces;
using HouseControl.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Dynamic;
using System.Globalization;
using System.Text.Json;

namespace HouseControl.UI.Controllers
{
    public class LancamentosController : Controller
    {
        private readonly ILancamentoRepository _lancamentoRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public LancamentosController(ILancamentoRepository repository, ICategoriaRepository categoriaRepository)
        {
            _lancamentoRepository = repository;
            _categoriaRepository = categoriaRepository;
        }

        public IActionResult Index()
        {
            ViewData["title"] = "Lançamentos";
            List<ExpandoObject> listaRetorno = GetLancamentosList();
            ViewBag.lista = listaRetorno;
            return View();
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            ViewData["title"] = "Cadastrar Lancamento";
            ViewBag.header = new HeaderViewModel { Title = "Cadastrar Lancamento" };
            ViewBag.categorias = new SelectList(_categoriaRepository.GetAllAsync().Result.ToList().OrderBy(x => x.Nome), "Id", "Nome");
            ViewBag.Erro = null;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(Domain.Entities.Lancamento lancamento)
        {
            if (ModelState.IsValid)
            {
                await _lancamentoRepository.CreateLancamentoAsync(lancamento);
                return RedirectToAction("Index");
            }

            ViewBag.Erro = "erro";
            ViewBag.categorias = new SelectList(_categoriaRepository.GetAllAsync().Result.ToList().OrderBy(x => x.Nome), "Id", "Nome", lancamento.CategoriaId);
            return View(lancamento);
        }

        [HttpGet]
        public async Task<IActionResult> Alterar(Guid id)
        {
            ViewData["title"] = "Alterar Lancamento";
            ViewBag.header = new HeaderViewModel { Title = "Alterar Lancamento" };
            ViewBag.categorias = new SelectList(_categoriaRepository.GetAllAsync().Result.ToList().OrderBy(x => x.Nome), "Id", "Nome");

            var lancamento = await _lancamentoRepository.GetByIdAsync(id);

            return View(lancamento);
        }

        [HttpPost]
        public IActionResult Alterar(Domain.Entities.Lancamento lancamento)
        {
            if (ModelState.IsValid)
            {
                _lancamentoRepository.UpdateLancamentoAsync(lancamento);
                return RedirectToAction("Index");
            }

            return View(lancamento);
        }

        [HttpGet]
        public async Task<IActionResult> Remover(Guid id)
        {
            ViewData["title"] = "Alterar Lancamento";
            ViewBag.header = new HeaderViewModel { Title = "Alterar Lancamento" };
            ViewBag.categorias = new SelectList(_categoriaRepository.GetAllAsync().Result.ToList().OrderBy(x => x.Nome), "Id", "Nome");

            var _lanc = await _lancamentoRepository.GetByIdAsync(id);

            return View(_lanc);
        }

        [HttpPost]
        public async Task<IActionResult> Remover(Domain.Entities.Lancamento lancamento)
        {
            await _lancamentoRepository.DeleteLancamento(lancamento.Id);
            return RedirectToAction("Index");
        }

        public JsonResult GetLancamentos()
        {
            List<ExpandoObject> listaRetorno = GetLancamentosList();

            return Json(listaRetorno);
        }

        private List<ExpandoObject> GetLancamentosList()
        {
            List<ExpandoObject> listaRetorno = new List<ExpandoObject>();
            var lancamentos = _lancamentoRepository.GetAllAsync().Result.ToList().OrderByDescending(x => x.DtLancamento);

            foreach (var item in lancamentos)
            {
                dynamic _lanc = new ExpandoObject();
                _lanc.id = item.Id;
                _lanc.dtLancamento = item.DtLancamento.ToString("dd/MM/yyyy");
                _lanc.ano = item.DtLancamento.Year;
                _lanc.mes = item.DtLancamento.Month;
                var mesNome = item.DtLancamento.ToString("MMMM", new CultureInfo("pt-BR"));
                mesNome = char.ToUpper(mesNome[0]) + mesNome.Substring(1);
                _lanc.mesNome = mesNome;
                _lanc.quinzena = item.DtLancamento.Day > 15 ? 2 : 1;
                _lanc.valor = item.Valor.ToString("C");
                _lanc.categoria = item.Categoria?.Nome;
                _lanc.categoriaId = item.CategoriaId;
                _lanc.parcela = item.Parcela;
                _lanc.descricao = item.Descricao.Trim();
                _lanc.totalParcelas = item.TotalParcelas;

                listaRetorno.Add(_lanc);
            }

            return listaRetorno;
        }
    }
}
