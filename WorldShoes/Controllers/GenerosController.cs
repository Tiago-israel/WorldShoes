using MVC.Model.DataBase;
using MVC.Model.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorldShoes.Controllers
{
    public class GenerosController : Controller
    {
        // GET: Generos
        public ActionResult Index()
        {
            var generos = DBConfig.Instance.GeneroRepository.FindAll();
            return View(generos);
        }

        // GET: Generos/Details/5
        public ActionResult Details(int id)
        {
            Genero genero = DBConfig.Instance.GeneroRepository.PrimeiroGenero(id);
            if (genero != null)
            {
                return View(genero);
            }
            return RedirectToAction("Index");
        }

        // GET: Generos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Generos/Create
        [HttpPost]
        public ActionResult Create(Genero genero)
        {
            if (ModelState.IsValid)
            {
                DBConfig.Instance.GeneroRepository.Salvar(genero);
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Generos/Edit/5
        public ActionResult Edit(int id)
        {
            Genero genero = DBConfig.Instance.GeneroRepository.PrimeiroGenero(id);
            if (genero != null)
            {
                return View(genero);
            }
            return RedirectToAction("Index");
        }

        // POST: Generos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Genero genero)
        {
            try
            {
                DBConfig.Instance.GeneroRepository.Salvar(genero);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Generos/Delete/5
        public ActionResult Delete(int id)
        {
            Genero genero = DBConfig.Instance.GeneroRepository.PrimeiroGenero(id);
           
                if (genero != null)
                {
                    return View(genero);
                }
                return RedirectToAction("Index");
        }

        // POST: Generos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Genero genero)
        {
            try
            {
                genero = DBConfig.Instance.GeneroRepository.PrimeiroGenero(genero.Id);
                DBConfig.Instance.GeneroRepository.Excluir(genero);
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ErrorMessage"] = "Você tem produtos atrelado à este gênero";
                return View(genero);
            }
            
        }
    }
}