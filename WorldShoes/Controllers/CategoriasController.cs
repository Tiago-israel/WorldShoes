using MVC.Model.DataBase;
using MVC.Model.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorldShoes.Controllers
{
    public class CategoriasController : Controller
    {
        // GET: Categorias
        public ActionResult Index()
        {
            var categorias = DBConfig.Instance.CategoriaRepository.FindAll();
            return View(categorias);
        }

        // GET: Categorias/Details/5
        public ActionResult Details(int id)
        {
            Categoria categoria = DBConfig.Instance.CategoriaRepository.PrimeiraCategoria(id);
            if (categoria != null)
            {
                return View(categoria);
            }
            return RedirectToAction("Index");
        }

        // GET: Categorias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        [HttpPost]
        public ActionResult Create(Categoria categoria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DBConfig.Instance.CategoriaRepository.Salvar(categoria);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Categorias/Edit/5
        public ActionResult Edit(int id)
        {
            Categoria categoria = DBConfig.Instance.CategoriaRepository.PrimeiraCategoria(id);
            if (categoria != null)
            {
                return View(categoria);
            }
            return RedirectToAction("Index");
        }

        // POST: Categorias/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Categoria categoria)
        {
            try
            {
                // TODO: Add update logic here
                DBConfig.Instance.CategoriaRepository.Salvar(categoria);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Categorias/Delete/5
        public ActionResult Delete(int id)
        {
            Categoria categoria = DBConfig.Instance.CategoriaRepository.PrimeiraCategoria(id);
            if (categoria != null)
            {
                return View(categoria);
            }
            return RedirectToAction("Index");
        }

        // POST: Categorias/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Categoria categoria)
        {
            try
            {
                // TODO: Add delete logic here
                categoria = DBConfig.Instance.CategoriaRepository.PrimeiraCategoria(categoria.Id);
                DBConfig.Instance.CategoriaRepository.Excluir(categoria);
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ErrorMessage"] = "Você tem produtos atrelado à esta categoria";
                return View(categoria);
            }
        }
    }
}