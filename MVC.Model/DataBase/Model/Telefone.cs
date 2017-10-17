using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Model.DataBase.Model
{
    public class Telefone
    {
        public virtual int Id { get; set; }

        public virtual String Numero { get; set; }
        public virtual String Tipo { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual Fabricante Fabricante { get; set; }
    }

    public class TelefoneMap : ClassMapping<Telefone>
    {
        public TelefoneMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Identity));

            Property(x => x.Numero, m =>
            {

                m.NotNullable(true);
            });
            Property(x => x.Tipo, m =>
            {
                m.NotNullable(true);
            });

            ManyToOne(x => x.Usuario, m =>
            {
                m.Column("idUsuario");
            });

            ManyToOne(x => x.Fabricante, m =>
            {
                m.Column("idFabricante");
            });



        }
    }
}
