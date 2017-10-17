using MVC.Model.DataBase;
using MVC.Model.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorldShoes.Controllers
{
    public class FabricantesController : Controller
    {
        // GET: Categorias
        public ActionResult Index()
        {
            var fabricantes = DBConfig.Instance.FabricanteRepository.FindAll();
            return View(fabricantes);
        }

        // GET: Categorias/Details/5
        public ActionResult Details(int id)
        {
            Fabricante fabricante = DBConfig.Instance.FabricanteRepository.PrimeiroFabricante(id);
            if (fabricante != null)
            {
                return View(fabricante);
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
        public ActionResult Create(Fabricante fabricante)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DBConfig.Instance.FabricanteRepository.Salvar(fabricante);
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
            Fabricante fabbricante = DBConfig.Instance.FabricanteRepository.PrimeiroFabricante(id);
            if (fabbricante != null)
            {
                return View(fabbricante);
            }
            return RedirectToAction("Index");
        }

        // POST: Categorias/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Fabricante fabricante)
        {
            try
            {
                // TODO: Add update logic here
                DBConfig.Instance.FabricanteRepository.Salvar(fabricante);
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
            Fabricante fabbricante = DBConfig.Instance.FabricanteRepository.PrimeiroFabricante(id);
            if (fabbricante != null)
            {
                return View(fabbricante);
            }
            return RedirectToAction("Index");
        }

        // POST: Categorias/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Fabricante fabricante)
        {
            try
            {
                // TODO: Add delete logic here
                fabricante = DBConfig.Instance.FabricanteRepository.PrimeiroFabricante(fabricante.Id);
                DBConfig.Instance.FabricanteRepository.Excluir(fabricante);
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ErrorMessage"] = "Você tem produtos atrelado à esta fabricante";
                return View(fabricante);
            }
        }
    }
}