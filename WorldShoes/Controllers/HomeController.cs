using MVC.Model.DataBase;
using MVC.Model.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using WorldShoes.Helpers;

namespace WorldShoes.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int page = 1, int pageSize = 9)
        {
            this.PreencherCategoriasEFabricantes();
            var produtos = (List<Produto>)DBConfig.Instance.ProdutoRepository.AgruparPorNome();
            Random random = new Random();
            produtos = produtos.OrderBy(p => random.Next()).ToList();
            PagedList<Produto> paginacao = new PagedList<Produto>(produtos, page, pageSize);
            ViewBag.Acao = "Index";
            return View(paginacao);
        }

        public ActionResult ProdutosPorCategoria(int id, int page = 1, int pageSize = 9)
        {
            PreencherCategoriasEFabricantes();
            var produtos = DBConfig.Instance.ProdutoRepository.BuscarPorCategoria(id);
            PagedList<Produto> paginacao = new PagedList<Produto>(produtos, page, pageSize);
            ViewBag.Id = id;
            ViewBag.Acao = "ProdutosPorCategoria";
            return View(paginacao);
        }

        public ActionResult ProdutosPorMarca(int id, int page = 1, int pageSize = 9)
        {
            PreencherCategoriasEFabricantes();
            var produtos = DBConfig.Instance.ProdutoRepository.BuscarPorMarca(id);
            PagedList<Produto> paginacao = new PagedList<Produto>(produtos, page, pageSize);
            ViewBag.Id = id;
            ViewBag.Acao = "ProdutosPorMarca";
            return View(paginacao);
        }

        public ActionResult ProdutosPorDescricao(string busca, int page = 1, int pageSize = 9)
        {
            if (Session["Cliente"] != null)
            {
                SalvarHistorico(busca);
            }
            PreencherCategoriasEFabricantes();
            var produtos = DBConfig.Instance.ProdutoRepository.FindAll().Where(p => p.Nome.ToUpper().Contains(busca.ToUpper()) || p.Descricao.ToUpper().Contains(busca.ToUpper()));
            PagedList<Produto> paginacao = new PagedList<Produto>(produtos, page, pageSize);
            ViewBag.Acao = "ProdutosPorDescricao";
            return View(paginacao);
        }


        public ActionResult DetalhesProduto(int id = 0)
        {
            Produto produto = DBConfig.Instance.ProdutoRepository.PrimeiroProduto(id);
            if (produto != null)
            {
                var produtosRelacionados = DBConfig.Instance.ProdutoRepository.FindAll().Where(p => Equals(p.Categoria.Nome, produto.Categoria.Nome)).ToList();
                produtosRelacionados.Remove(produto);
                ViewBag.produtosRelacionados = produtosRelacionados;
                ViewBag.produtoComOutrasCores = DBConfig.Instance.ProdutoRepository.FindAll().Where(p => Equals(p.Nome, produto.Nome)).ToList();
                return View(produto);
            }
            return RedirectToAction("Produtos");
        }

        private void PreencherCategoriasEFabricantes()
        {
            ViewBag.Categorias = DBConfig.Instance.CategoriaRepository.FindAll().Where(c => c.Produtos.Count() > 0).OrderBy(c => c.Nome);
            ViewBag.Fabricantes = DBConfig.Instance.FabricanteRepository.FindAll().Where(f => f.Produtos.Count() > 0).OrderBy(f => f.razao_social);
        }
        [HttpPost]
        public ActionResult AdicionarComentario(int idProduto, Avaliacao avaliacao)
        {
            avaliacao.Usuario = (Usuario)Session["Cliente"];
            avaliacao.Produto = DBConfig.Instance.ProdutoRepository.PrimeiroProduto(idProduto);
            DBConfig.Instance.AvaliacaoRepository.Salvar(avaliacao);
            return RedirectToAction("Index");
        }


        public void SalvarHistorico(string busca)
        {
            HistoricoBusca historico = new HistoricoBusca();
            historico.Data = DateTime.Now;
            historico.Titulo = busca;
            historico.Usuario = (Usuario)Session["Cliente"];
            historico.Termo = "Produto";
            DBConfig.Instance.HistoricoRepository.Salvar(historico);
        }

        public ActionResult Carrinho()
        {
            var carrinho = ObterCarrinhoEmCookies();
            CarregarEnderecosCliente();
            CalcularTotalProdutos(carrinho);
            ViewBag.Carrinho = carrinho;
            return View(carrinho);

        }

        private void CarregarEnderecosCliente()
        {
            var cliente = (Usuario)Session["Cliente"];
            List<Endereco> enderecos = new List<Endereco>();
            ViewBag.Enderecos = enderecos;
            if (cliente != null)
            {
                cliente = DBConfig.Instance.UsuarioRepository.PrimeiroUsuario(cliente.Id);
                Session["Cliente"] = cliente;
                foreach (var endereco in cliente.Enderecos)
                {
                    enderecos.Add(endereco);
                }
                ViewBag.Enderecos = enderecos;
            }

        }

        private void CalcularTotalProdutos(List<ItemCarrinho> carrinho)
        {
            ViewData["total"] = 0;
            decimal total = 0;
            foreach (var item in carrinho)
            {
                total += item.Total;
            }
            ViewData["total"] = total;
            ViewData["total_frete"] = total + 10;
        }

        public ActionResult AdicionarAoCarrinho(int id)
        {
            var produto = DBConfig.Instance.ProdutoRepository.PrimeiroProduto(id);
            var carrinho = new List<ItemCarrinho>();

            if (produto != null)
            {
                carrinho = ObterCarrinhoEmCookies();
                List<String> itensIds = new List<string>();
                if (carrinho == null)
                {
                    var item = new ItemCarrinho()
                    {
                        Produto = produto,
                        Quantidade = 1,
                        Total = produto.Valor
                    };

                    itensIds.Add(Convert.ToString(item.Produto.Id));
                    GravarCookiesCarrinho(itensIds);
                }
                else
                {
                    var aux = carrinho.FirstOrDefault<ItemCarrinho>(p => p.Produto.Id == id);
                    if (aux == null)
                    {
                        var item = new ItemCarrinho()
                        {
                            Produto = produto,
                            Quantidade = 1,
                            Total = produto.Valor
                        };
                        carrinho.Add(item);
                        foreach (var itemCar in carrinho)
                        {
                            itensIds.Add(Convert.ToString(itemCar.Produto.Id) + ">" + itemCar.Quantidade);
                        }
                        GravarCookiesCarrinho(itensIds);
                    }

                }
            }
            return RedirectToAction("Carrinho");
        }

        public ActionResult RemoverItemDoCarrinho(int id)
        {
            List<ItemCarrinho> carrinho = ObterCarrinhoEmCookies();
            if (carrinho != null)
            {
                var item = carrinho.FirstOrDefault<ItemCarrinho>(p => p.Produto.Id == id);
                carrinho.Remove(item);
                List<String> itensIds = new List<string>();
                foreach (var itemCar in carrinho)
                {
                    itensIds.Add(Convert.ToString(itemCar.Produto.Id) + ">" + itemCar.Quantidade);
                }
                GravarCookiesCarrinho(itensIds);
            }

            return RedirectToAction("Carrinho");
        }

        [HttpPost]
        public ActionResult AtualizarItem(int idProduto)
        {
            int quantidade = Convert.ToInt32(Request["quantity"]);
            if (quantidade == 0 || quantidade < 0)
            {
                quantidade = 1;
            }
            List<String> itensIds = new List<string>();
            List<ItemCarrinho> carrinho = ObterCarrinhoEmCookies();
            ItemCarrinho item = carrinho.FirstOrDefault<ItemCarrinho>(p => p.Produto.Id == idProduto);
            if (item != null)
            {

                item.Quantidade = quantidade;
                item.Total = item.Produto.Valor * item.Quantidade;
                foreach (var itemCar in carrinho)
                {
                    itensIds.Add(Convert.ToString(itemCar.Produto.Id) + ">" + itemCar.Quantidade);
                }
                GravarCookiesCarrinho(itensIds);

            }

            return RedirectToAction("Carrinho");
        }


        private void GravarCookiesCarrinho(List<String> itens)
        {
            HttpCookie carrinho_cookie = Request.Cookies["carrinho"];

            carrinho_cookie = new HttpCookie("carrinho");
            var count = 0;
            foreach (var item in itens)
            {
                carrinho_cookie.Values.Add(count + "", item);
                count++;
            }
            carrinho_cookie.Expires = DateTime.Now.AddDays(7);
            carrinho_cookie.HttpOnly = true;
            Response.AppendCookie(carrinho_cookie);

        }

        private List<ItemCarrinho> ObterCarrinhoEmCookies()
        {
            HttpCookie cookies = Request.Cookies["carrinho"];
            var itemCarrinhos = new List<ItemCarrinho>();
            if (cookies != null)
            {
                String[] produtosIds = cookies.Value.ToString().Split('&');
                if(produtosIds[0] != "")
                {
                    for (int i = 0; i < produtosIds.Length; i++)
                    {
                        char id = '0';
                        char quantidade = '0';
                        char aux = '0';
                        for (int j = 0; j < produtosIds[i].Length; j++)
                        {
                            aux = produtosIds[i][j];
                            if (aux.Equals('='))
                            {
                                id = produtosIds[i][j + 1];
                            }
                            if (aux.Equals('>'))
                            {
                               
                                quantidade = produtosIds[i][j + 1];
                            }
                        }
                        var produto = DBConfig.Instance.ProdutoRepository.PrimeiroProduto((int)Char.GetNumericValue(id));
                        var qtd = (int)Char.GetNumericValue(quantidade);
                        itemCarrinhos.Add(new ItemCarrinho()
                        {
                            Produto = produto,
                            Quantidade = qtd,
                            Total = produto.Valor * qtd
                        });

                    }
                }
                
            }
            return itemCarrinhos;
        }

        private void removerCookie()
        {
            Response.Cookies["carrinho"].Expires = DateTime.Now.AddDays(-1);
        }

        public ActionResult SalvarEnderecoCarrinho(String cep, int numero, String Rua, String Bairro, String Cidade, String Estado)
        {
            var endereco = new Endereco()
            {
                Cep = cep,
                Numero = numero,
                Rua = Rua,
                Bairro = Bairro,
                Cidade = Cidade,
                Estado = Estado,
                Usuario = (Usuario)Session["Cliente"]
            };
            DBConfig.Instance.EnderecoRepository.Salvar(endereco);
            return RedirectToAction("Carrinho");
        }


        [HttpPost]
        public ActionResult FinalizarPedido(int endereco)
        {
            var itensCarrinho = ObterCarrinhoEmCookies();

            var usuario = (Usuario)Session["Cliente"];
            var pedido = new Pedido()
            {
                Usuario = usuario,
                DataPedido = DateTime.Now,
                LocalEntrega = DBConfig.Instance.EnderecoRepository.PrimeiroEndereco(endereco)
            };
            DBConfig.Instance.PedidoRepository.Salvar(pedido);
            foreach(var item in itensCarrinho)
            {
                var PedidoProduto = new PedidoProduto()
                {
                    Pedido = pedido,
                    Produto = item.Produto,
                    Quantidade = item.Quantidade,
                    Total = item.Total
                };
                DBConfig.Instance.PedidoProdutoRepository.Salvar(PedidoProduto);
            }
            removerCookie();
            return RedirectToAction("Index");
        }

    }
}