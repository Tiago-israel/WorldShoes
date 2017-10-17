using MVC.Model.DataBase.Model;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Model.DataBase.Repository
{
    public class AdministradorRepository : RepositoryBase<Administrador>
    {
        public AdministradorRepository(ISession session) : base(session)
        {

        }

        public Administrador PrimeiroAdministrador(int id)
        {
            var administrador = this.Session.Query<Administrador>().FirstOrDefault(f => f.Id == id);

            return administrador;
        }
    }
}
