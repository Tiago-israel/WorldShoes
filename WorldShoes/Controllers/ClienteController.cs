using MVC.Model.DataBase;
using MVC.Model.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WorldShoes.Helpers;

namespace WorldShoes.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult CreateCliente()
        {
            var u = new Usuario();

            return View(u);
        }

        public ActionResult GravarCliente(Usuario u)
        {
            var procurar = DBConfig.Instance.UsuarioRepository.FindAll().FirstOrDefault(f => f.Cpf == u.Cpf);
            if (procurar != null)
            {
                ViewBag.Message = "CPF já cadastrado";

                return View("CreateCliente");
            }
            else
            {
                u.Senha = GeradorMD5.GetMd5HashData(u.Senha);
                DBConfig.Instance.UsuarioRepository.Salvar(u);
                return View("Index", u);
            }
        }

        public ActionResult EditCliente(int id)
        {
            var u = DBConfig.Instance.UsuarioRepository.PrimeiroUsuario(id);

            return View(u);
        }

        public ActionResult UpdateCliente(int id, Usuario usuario)
        {

            DBConfig.Instance.UsuarioRepository.Salvar(usuario);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DetailsCliente()
        {
            Usuario usuario = (Usuario)Session["Cliente"];
            var u = DBConfig.Instance.UsuarioRepository.FindAll().FirstOrDefault(c => c.Id == usuario.Id);
            return View(u);
        }

        public ActionResult Login(String email, String senha)
        {
            senha = GeradorMD5.GetMd5HashData(senha);
            var u = DBConfig.Instance.UsuarioRepository.findLoginAndSenha(email, senha);
            if (u != null)
            {
                ViewBag.Message = "Pessoa Encontrada";
                Session["Cliente"] = u;
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Message = "Ops! Usuário não encontrado. Confira seus dados ou crie uma conta";
            return View("CreateCliente");
        }

        public ActionResult logoff()
        {
            Session.Remove("Cliente");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult HistoricoBuscas() {
            Usuario usuario = (Usuario)Session["Cliente"];
            List<HistoricoBusca> buscas = DBConfig.Instance.HistoricoRepository.FindAll().Where(h => h.Usuario.Id == usuario.Id).ToList();
            return View(buscas);
        }

    }
}