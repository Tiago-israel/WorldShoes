using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Model.DataBase.Model
{
    public class Administrador
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "O Campo Nome é obrigatório")]
        public virtual String Nome { get; set; }

        [Required(ErrorMessage = "O Campo Email é obrigatório")]
        [DataType(DataType.EmailAddress)]
        public virtual String Email { get; set; }
    }

    public class AdministradorMap : ClassMapping<Administrador>
    {
        public AdministradorMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Identity));

            Property(x => x.Nome);
            Property(x => x.Nome, m =>
            {
                m.NotNullable(true);
            });
            Property(x => x.Email, m =>
            {
                m.Unique(true);
                m.NotNullable(true);
            });
        }
    }
}
