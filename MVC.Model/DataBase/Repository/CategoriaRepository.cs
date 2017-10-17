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
    public class CategoriaRepository : RepositoryBase<Categoria>
    {
        public CategoriaRepository(ISession session) : base(session)
        {
        }

        public Categoria PrimeiraCategoria(int id)
        {
            var categoria = this.Session.Query<Categoria>().FirstOrDefault(f => f.Id == id);

            return categoria;
        }
    }
}
