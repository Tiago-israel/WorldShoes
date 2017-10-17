using MVC.Model.DataBase;
using MVC.Model.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorldShoes.Controllers
{
    public class EnderecosController : Controller
    {
        public ActionResult MeusEnderecos(int idUsuario)
        {
            var enderecos = DBConfig.Instance.EnderecoRepository.FindAll().Where(e => e.Usuario.Id == idUsuario).ToList();
            return View(enderecos);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult SaveEndereco(Endereco endereco)
        {
            Usuario usuario = (Usuario)Session["Cliente"];
            if(usuario != null)
            {
                endereco.Usuario = usuario;
                DBConfig.Instance.EnderecoRepository.Salvar(endereco);

            }
            return RedirectToAction("Index","Home");
        }


    }
}