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
    public class TelefoneRepository : RepositoryBase<Telefone>
    {
        public TelefoneRepository(ISession session) : base(session)
        {
        }

        public Telefone PrimeiroUsuario(int id)
        {
            var telefone = this.Session.Query<Telefone>().FirstOrDefault(f => f.Id == id);

            return telefone;
        }
    }
}
