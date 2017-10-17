using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Model.DataBase.Model
{
    public class Preferencia
    {
        public virtual int Id { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual Fabricante Fabricante { get; set; }
    }

    public class PreferenciaMap : ClassMapping<Preferencia>
    {
        public PreferenciaMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Identity));

            ManyToOne(x => x.Usuario, m =>
            {
                m.NotNullable(true);
                m.Column("idUsuario");
            });

            ManyToOne(x => x.Categoria, m =>
            {
                m.NotNullable(false);
                m.Column("idCategoria");
            });

            ManyToOne(x => x.Fabricante, m =>
            {
                m.NotNullable(false);
                m.Column("idFabricante");
            });
        }
    }
}