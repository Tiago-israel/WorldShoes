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
    public class FotoProdutoRepository : RepositoryBase<FotoProduto>
    {
        public FotoProdutoRepository(ISession session) : base(session)
        {
        }

        public FotoProduto PrimeiraFotoProduto(int id)
        {
            var fotoProduto = this.Session.Query<FotoProduto>().FirstOrDefault(f => f.Id == id);

            return fotoProduto;
        }
    }
}
