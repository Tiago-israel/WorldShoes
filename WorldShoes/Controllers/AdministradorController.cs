using MVC.Model.DataBase;
using MVC.Model.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorldShoes.Helpers;

namespace WorldShoes.Controllers
{
    public class AdministradorController : Controller
    {
        // GET: Administrador/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrador/Create
        [HttpPost]
        public ActionResult Create(Administrador admin, string senha)
        {
            SenhaAdmin sa = new SenhaAdmin();

            if (Equals(senha, sa.CriarNovaSenhaAdmin()))
            {
                DBConfig.Instance.AdministradorRepository.Salvar(admin);
                ViewBag.Message = "Administrador salvo com sucesso!";
                return RedirectToAction("Login");
            }
            TempData["ErrorMessage"] = "Senha Incorreta !";
            return RedirectToAction("Create");
        }



        // POST: Administrador/Delete/5


        public ActionResult Login()
        {
            return View();
        }


        public ActionResult VerificarLogin(Administrador admin, string senha)
        {
            SenhaAdmin sa = new SenhaAdmin();
            if (Equals(senha, sa.CriarNovaSenhaAdmin()))
            {
                var administrador = DBConfig.Instance.AdministradorRepository.FindAll().FirstOrDefault(f => f.Email == admin.Email);
                if (administrador != null)
                {
                    Session["Administrador"] = administrador;
                    return RedirectToAction("Index", "Produtos");
                }

            }
            TempData["ErrorMessage"] = "Login ou senha inválidos";
            return RedirectToAction("Login");
        }



        public ActionResult Logout()
        {
            Session.Remove("Administrador");
            return RedirectToAction("Login");
        }
    }
}