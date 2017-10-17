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
    public class UsuarioRepository : RepositoryBase<Usuario>
    {
        public UsuarioRepository(ISession session) : base(session)
        {
        }

        public Usuario PrimeiroUsuario(int id)
        {
            var usuario = this.Session.Query<Usuario>().FirstOrDefault(f => f.Id == id);

            return usuario;
        }

        public Usuario findLoginAndSenha(String email, String senha)
        {
            var usuario = this.Session.Query<Usuario>().Where(u => u.Email.Equals(email) && u.Senha.Equals(senha)).FirstOrDefault();
            return usuario;

        }

    }



}
