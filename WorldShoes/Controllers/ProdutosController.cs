using MVC.Model.DataBase;
using MVC.Model.DataBase.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorldShoes.Controllers
{
    public class ProdutosController : Controller
    {
        // GET: Categorias
        public ActionResult Index()
        {
            var produtos = DBConfig.Instance.ProdutoRepository.FindAll();
            return View(produtos);
        }

        // GET: Categorias/Details/5
        public ActionResult Details(int id)
        {
            Produto produto = DBConfig.Instance.ProdutoRepository.PrimeiroProduto(id);
            if (produto != null)
            {
                return View(produto);
            }
            return RedirectToAction("Index");
        }

        // GET: Categorias/Create
        public ActionResult Create()
        {
            ViewBag.categoria_id = new SelectList(DBConfig.Instance.CategoriaRepository.FindAll(), "Id", "nome");
            ViewBag.cor_id = new SelectList(DBConfig.Instance.CorRepository.FindAll(), "Id", "nome");
            ViewBag.Fabricante_id = new SelectList(DBConfig.Instance.FabricanteRepository.FindAll(), "Id", "razao_social");
            ViewBag.genero_id = new SelectList(DBConfig.Instance.GeneroRepository.FindAll(), "Id", "Nome");
            return View();
        }

        // POST: Categorias/Create
        [HttpPost]
        public ActionResult Create(Produto produto)
        {
            Cor cor = DBConfig.Instance.CorRepository.PrimeiraCor(Convert.ToInt32(Request["cor_id"]));
            Categoria categoria = DBConfig.Instance.CategoriaRepository.PrimeiraCategoria(Convert.ToInt32(Request["categoria_id"]));
            Fabricante fabricante = DBConfig.Instance.FabricanteRepository.PrimeiroFabricante(Convert.ToInt32(Request["fabricante_id"]));
            Genero genero = DBConfig.Instance.GeneroRepository.PrimeiroGenero(Convert.ToInt32(Request["genero_id"]));
            if (cor != null && categoria != null && fabricante != null && genero != null)
            {
                //decimal valor = Convert.ToDecimal(Request["Valor"].Replace(".",","));
                //produto.Valor = valor;
                produto.Categoria = categoria;
                produto.Cor = cor;
                produto.Fabricante = fabricante;
                produto.Genero = genero;
                DBConfig.Instance.ProdutoRepository.Salvar(produto);
                produto.FotosProduto = BuscarESalvarImagens(produto);
                DBConfig.Instance.ProdutoRepository.Salvar(produto);
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Categorias/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Produto produto = DBConfig.Instance.ProdutoRepository.PrimeiroProduto(id);
            if (produto != null)
            {
                ViewBag.categoria_id = new SelectList(DBConfig.Instance.CategoriaRepository.FindAll(), "Id", "nome", produto.Categoria.Id);
                ViewBag.cor_id = new SelectList(DBConfig.Instance.CorRepository.FindAll(), "Id", "nome", produto.Cor.Id);
                ViewBag.Fabricante_id = new SelectList(DBConfig.Instance.FabricanteRepository.FindAll(), "Id", "razao_social", produto.Fabricante.Id);
                ViewBag.genero_id = new SelectList(DBConfig.Instance.GeneroRepository.FindAll(), "Id", "nome", produto.Genero.Id);
                
                return View(produto);
            }
            return RedirectToAction("Index");
        }

        // POST: Categorias/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Produto produto)
        {
            try
            {
                // TODO: Add update logic here
                Cor cor = DBConfig.Instance.CorRepository.PrimeiraCor(Convert.ToInt32(Request["cor_id"]));
                Categoria categoria = DBConfig.Instance.CategoriaRepository.PrimeiraCategoria(Convert.ToInt32(Request["categoria_id"]));
                Fabricante fabricante = DBConfig.Instance.FabricanteRepository.PrimeiroFabricante(Convert.ToInt32(Request["fabricante_id"]));
                Genero genero = DBConfig.Instance.GeneroRepository.PrimeiroGenero(Convert.ToInt32(Request["genero_id"]));
                List<FotoProduto> fotos = DBConfig.Instance.FotoProdutoRepository.FindAll().Where(f => f.Produto.Id == id).ToList();
                if (cor != null && categoria != null && fabricante != null && genero != null)
                {
                    produto.Categoria = categoria;
                    produto.Cor = cor;
                    produto.Fabricante = fabricante;
                    produto.Genero = genero;
                    produto.FotosProduto = fotos;
                    DBConfig.Instance.ProdutoRepository.Salvar(produto);
                }
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
            Produto produto = DBConfig.Instance.ProdutoRepository.PrimeiroProduto(id);
            if (produto != null)
            {
                return View(produto);
            }
            return RedirectToAction("Index");
        }

        // POST: Categorias/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Produto produto)
        {
            try
            {
                // TODO: Add delete logic here
                //List<FotoProduto> fotos = DBConfig.Instance.FotoProdutoRepository.FindAll().Where(f => f.Produto.Id == id).ToList();
                //fotos.ForEach(f => DBConfig.Instance.FotoProdutoRepository.Excluir(f));
                produto = DBConfig.Instance.ProdutoRepository.PrimeiroProduto(produto.Id);
                DBConfig.Instance.ProdutoRepository.Excluir(produto);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(produto);
            }
        }

        private List<FotoProduto> BuscarESalvarImagens(Produto produto)
        {
            List<FotoProduto> fotos = new List<FotoProduto>();
            HttpPostedFileBase arquivo = null;
            for (int i = 0; i < Request.Files.Count; i++)
            {
                arquivo = Request.Files[i];
                if (arquivo.ContentLength > 0)
                {
                    var uploadPath = Server.MapPath("~/Content/img");
                    string caminhoArquivo = Path.Combine(@uploadPath, Path.GetFileName(arquivo.FileName));
                    arquivo.SaveAs(caminhoArquivo);
                    fotos.Add(new FotoProduto
                    {
                        Imagem = arquivo.FileName,
                        Produto = produto
                    });
                }
            }
            return fotos;
        }
    }
}