using MVC.Model.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Linq;

namespace MVC.Model.DataBase.Repository
{
    public class ProdutoRepository : RepositoryBase<Produto>
    {
        public ProdutoRepository(ISession session) : base(session)
        {
        }

        public Produto PrimeiroProduto(int id)
        {
            var produto = this.Session.Query<Produto>().FirstOrDefault(f => f.Id == id);

            return produto;
        }

        public IList<Produto> AgruparPorNome()
        {
            var query = Session.CreateSQLQuery(@"SELECT p.* FROM worldshoes.produto as p group by Nome").AddEntity("p", typeof(Produto));
            return query.List<Produto>();
        }

        public IList<Produto> BuscarPorCategoria(int idcategoria)
        {
            var query = Session.CreateSQLQuery(@"SELECT p.* FROM worldshoes.produto as p Where p.idCategoria="+idcategoria+ " group by Nome").AddEntity("p", typeof(Produto));
            return query.List<Produto>();
        }


        public IList<Produto> BuscarPorDescricao(String descricao)
        {
            var query = Session.CreateSQLQuery(@"SELECT p.* FROM worldshoes.produto as p Where p.nome like '%" + descricao + "%'").AddEntity("p", typeof(Produto));
            return query.List<Produto>();
        }

        public IList<Produto> BuscarPorMarca(int idmarca)
        {
            var query = Session.CreateSQLQuery(@"SELECT p.* FROM worldshoes.produto as p Where p.idFabricante=" + idmarca + " group by Nome").AddEntity("p", typeof(Produto));
            return query.List<Produto>();
        }

    }
}
