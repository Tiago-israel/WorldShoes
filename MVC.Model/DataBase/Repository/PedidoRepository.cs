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
    public class PedidoRepository : RepositoryBase<Pedido>
    {
        public PedidoRepository(ISession session) : base(session)
        {
        }

        public Pedido PrimeiroUsuario(int id)
        {
            var pedido = this.Session.Query<Pedido>().FirstOrDefault(f => f.Id == id);

            return pedido;
        }
    }
}
