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
    public class GeneroRepository : RepositoryBase<Genero>
    {
        public GeneroRepository(ISession session) : base(session)
        {
        }

        public Genero PrimeiroGenero(int id)
        {
            var genero = this.Session.Query<Genero>().FirstOrDefault(f => f.Id == id);

            return genero;
        }
    }
}
