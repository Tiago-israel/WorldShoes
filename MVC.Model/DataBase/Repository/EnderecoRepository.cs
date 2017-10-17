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
    public class EnderecoRepository : RepositoryBase<Endereco>
    {
        public EnderecoRepository(ISession session) : base(session)
        {
        }


        public Endereco PrimeiroEndereco(int id)
        {
            var endereco = this.Session.Query<Endereco>().FirstOrDefault(f => f.Id == id);

            return endereco;
        }
    }
}
