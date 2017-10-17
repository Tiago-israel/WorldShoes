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
    public class CorRepository : RepositoryBase<Cor>
    {
        public CorRepository(ISession session) : base(session)
        {
        }

        public Cor PrimeiraCor(int id)
        {
            var cor = this.Session.Query<Cor>().FirstOrDefault(f => f.Id == id);

            return cor;
        }

    }
}
