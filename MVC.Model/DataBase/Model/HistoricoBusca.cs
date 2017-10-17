using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Model.DataBase.Model
{
    public class HistoricoBusca
    {
        public virtual int Id { get; set; }
        public virtual String Titulo { get; set; }
        public virtual String Termo { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual Usuario Usuario { get; set;}

        
    }

    public class HistoricoBuscaMap : ClassMapping<HistoricoBusca>
    {

        public HistoricoBuscaMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Identity));
            Property(x => x.Titulo, m =>
            {
                m.NotNullable(true);
            });
            Property(x => x.Data);

            Property(x => x.Termo);

            ManyToOne(x => x.Usuario, m =>
            {
                m.Column("idUsuario");
            });
        }

    }



}



