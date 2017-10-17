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
    public class FabricanteRepository : RepositoryBase<Fabricante>
    {
        public FabricanteRepository(ISession session) : base(session)
        {
        }

        public Fabricante PrimeiroFabricante(int id)
        {
            var fabricante = this.Session.Query<Fabricante>().FirstOrDefault(f => f.Id == id);

            return fabricante;
        }
    }
}
