using MVC.Model.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace MVC.Model.DataBase.Repository
{
    public class HistoricoRepository : RepositoryBase<HistoricoBusca>
    {
        public HistoricoRepository(ISession session) : base(session)
        {
        }
    }
}
