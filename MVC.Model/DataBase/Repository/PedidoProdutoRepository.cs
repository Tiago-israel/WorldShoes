using MVC.Model.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace MVC.Model.DataBase.Repository
{
    public class PedidoProdutoRepository : RepositoryBase<PedidoProduto>
    {
        public PedidoProdutoRepository(ISession session) : base(session)
        {

        }
    }
}
