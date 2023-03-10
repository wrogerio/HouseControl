using HouseControl.Domain.Entities;
using HouseControl.Infra.Interfaces;
using HouseControl.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace HouseControl.UI.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ICategoriaRepository _repository;

        public CategoriasController(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["title"] = "Categorias";

            return View();
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            ViewData["title"] = "Cadastrar Categoria";
            ViewBag.header = new HeaderViewModel { Title = "Cadastrar Categoria" };

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                var _categFind = await _repository.GetByNameAsync(categoria.Nome);
                if (_categFind.Nome != "") {
                    var erroModel = new ErrorViewModel {
                        ActionName = "Cadastrar",
                        ControllerName = "Categorias",
                        Mensagem = "Categoria já cadastrada",
                        Titulo = "Erro ao cadastrar categoria"
                    };
                    return RedirectToAction("Error", erroModel);
                }

                await _repository.CreateCategoriaAsync(categoria);
                return RedirectToAction("Index");
            }

            return View(categoria);
        }

        [HttpGet]
        public async Task<IActionResult> Alterar(Guid id)
        {
            ViewData["title"] = "Alterar Categoria";
            ViewBag.header = new HeaderViewModel { Title = "Alterar Categoria" };

            var categoria = await _repository.GetByIdAsync(id);

            return View(categoria);
        }

        [HttpPost]
        public IActionResult Alterar(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _repository.UpdateCategoriaAsync(categoria);
                return RedirectToAction("Index");
            }

            return View(categoria);
        }

        [HttpGet]
        public async Task<IActionResult> Remover(Guid id)
        {
            ViewData["title"] = "Remover Categoria";
            ViewBag.header = new HeaderViewModel { Title = "Remover Categoria" };

            var categoria = await _repository.GetByIdAsync(id);

            return View(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> Remover(Categoria categoria)
        {
            await _repository.DeleteCategoria(categoria.Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Error(ErrorViewModel erroModel)
        {
            return View(erroModel);
        }

        public JsonResult GetCategorias()
        {
            var categorias = _repository.GetAllAsync().Result.ToList().OrderBy(x => x.Nome);

            return Json(categorias);
        }
    }
}
