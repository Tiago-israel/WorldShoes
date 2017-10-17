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
    public class AvaliacaoRepository : RepositoryBase<Avaliacao>
    {
        public AvaliacaoRepository(ISession session) : base(session)
        {
        }

        public Avaliacao PrimeiraAvaliacao(int id)
        {
            var usuario = this.Session.Query<Avaliacao>().FirstOrDefault(f => f.Id == id);

            return usuario;
        }
    }
}
