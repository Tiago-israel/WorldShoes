using MVC.Model.DataBase.Model;
using MVC.Model.DataBase.Repository;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Model.DataBase.Repository
{
    public class PreferenciaRepository : RepositoryBase<Preferencia>
    {
        public PreferenciaRepository(ISession session) : base(session)
        {
        }

        public Preferencia PrimeiraPreferencia(int id)
        {
            var Preferencia = this.Session.Query<Preferencia>().FirstOrDefault(f => f.Id == id);

            return Preferencia;
        }

        public Preferencia PrimeiraPorCategoria(int id)
        {
            var Preferencia = this.Session.Query<Preferencia>().FirstOrDefault(f => f.Categoria.Id == id);

            return Preferencia;
        }

        public Preferencia PrimeiraPorMarca(int id)
        {
            var Preferencia = this.Session.Query<Preferencia>().FirstOrDefault(f => f.Fabricante.Id == id);

            return Preferencia;
        }
    }
}