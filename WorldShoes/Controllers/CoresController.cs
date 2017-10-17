using MVC.Model.DataBase;
using MVC.Model.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorldShoes.Controllers
{
    public class CoresController : Controller
    {
        // GET: Categorias
        public ActionResult Index()
        {
            var cores = DBConfig.Instance.CorRepository.FindAll();
            return View(cores);
        }

        // GET: Categorias/Details/5
        public ActionResult Details(int id)
        {
            Cor cor = DBConfig.Instance.CorRepository.PrimeiraCor(id);
            if (cor != null)
            {
                return View(cor);
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
        public ActionResult Create(Cor cor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DBConfig.Instance.CorRepository.Salvar(cor);
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
            Cor cor = DBConfig.Instance.CorRepository.PrimeiraCor(id);
            if (cor != null)
            {
                return View(cor);
            }
            return RedirectToAction("Index");
        }

        // POST: Categorias/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Cor cor)
        {
            try
            {
                // TODO: Add update logic here
                DBConfig.Instance.CorRepository.Salvar(cor);
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
            Cor cor = DBConfig.Instance.CorRepository.PrimeiraCor(id);
            if (cor != null)
            {
                return View(cor);
            }
            return RedirectToAction("Index");
        }

        // POST: Categorias/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Cor cor)
        {
            try
            {
                // TODO: Add delete logic here
                cor = DBConfig.Instance.CorRepository.PrimeiraCor(cor.Id);
                DBConfig.Instance.CorRepository.Excluir(cor);
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ErrorMessage"] = "Você tem produtos atrelado à esta cor";
                return View(cor);
            }
        }
    }
}