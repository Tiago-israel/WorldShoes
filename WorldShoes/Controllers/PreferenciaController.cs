using MVC.Model.DataBase;
using MVC.Model.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorldShoes.Controllers
{
    public class PreferenciaController : Controller
    {
        public ActionResult Index(int idUsuario)
        {
            var categorias = new List<Categoria>();
            ViewBag.Categorias = categorias;
            var marcas = new List<Fabricante>();
            ViewBag.Fabricantes = marcas;

            var preferencias = DBConfig.Instance.PreferenciaRepository.FindAll().Where(p => p.Usuario.Id == idUsuario).ToList();

            foreach(var item in preferencias)
            {
                if (item.Categoria != null)
                {
                    var cat = DBConfig.Instance.CategoriaRepository.PrimeiraCategoria(item.Categoria.Id);
                    categorias.Add(cat);
                }
                else if (item.Fabricante != null)
                {
                    var fab = DBConfig.Instance.FabricanteRepository.PrimeiroFabricante(item.Fabricante.Id);
                    marcas.Add(fab);
                }
            }

            ViewData["idUsuario"] = idUsuario;

            return View(preferencias);
        }

        public ActionResult Create(int idUsuario)
        {
            ViewData["idUsuario"] = idUsuario;
            ViewBag.Categorias = DBConfig.Instance.CategoriaRepository.FindAll();
            ViewBag.Fabricantes = DBConfig.Instance.FabricanteRepository.FindAll();
            return View();
        }

        public ActionResult preferenciaCategoria(int idCategoria, int idUsuario)
        {
            var preferencia = DBConfig.Instance.PreferenciaRepository.PrimeiraPorCategoria(idCategoria);

            if (preferencia == null)
            {
                var usu = DBConfig.Instance.UsuarioRepository.PrimeiroUsuario(idUsuario);
                var cat = DBConfig.Instance.CategoriaRepository.PrimeiraCategoria(idCategoria);

                var pref = new Preferencia();
                pref.Usuario = usu;
                pref.Categoria = cat;

                DBConfig.Instance.PreferenciaRepository.Salvar(pref);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult preferenciaFabricante(int idFabricante, int idUsuario)
        {
            var preferencia = DBConfig.Instance.PreferenciaRepository.PrimeiraPorMarca(idFabricante);

            if (preferencia == null)
            {
                var usu = DBConfig.Instance.UsuarioRepository.PrimeiroUsuario(idUsuario);
                var fab = DBConfig.Instance.FabricanteRepository.PrimeiroFabricante(idFabricante);

                var pref = new Preferencia();
                pref.Usuario = usu;
                pref.Fabricante = fab;

                DBConfig.Instance.PreferenciaRepository.Salvar(pref);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult DeletePorCategoria(int idCategoria)
        {
            var preferencia = DBConfig.Instance.PreferenciaRepository.PrimeiraPorCategoria(idCategoria);

            if (preferencia != null)
            {
                DBConfig.Instance.PreferenciaRepository.Excluir(preferencia);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult DeletePorFabricante(int idFabricante)
        {
            var preferencia = DBConfig.Instance.PreferenciaRepository.PrimeiraPorMarca(idFabricante);

            if (preferencia != null)
            {
                DBConfig.Instance.PreferenciaRepository.Excluir(preferencia);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
