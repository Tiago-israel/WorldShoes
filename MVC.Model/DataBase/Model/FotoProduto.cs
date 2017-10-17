using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Model.DataBase.Model
{
    public class FotoProduto
    {
        public virtual int Id { get; set; }
        public virtual String Imagem { get; set; }
        public virtual Usuario Usuario { get; set; }

        public virtual Produto Produto { get; set; }
    }

    public class FotoMap : ClassMapping<FotoProduto>
    {
        public FotoMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Identity));

            Property(x => x.Imagem, m =>
            {
                m.NotNullable(true);

            });

            ManyToOne(x => x.Produto, m =>
            {
                m.Column("idProduto");
            });

        }

    }
}
