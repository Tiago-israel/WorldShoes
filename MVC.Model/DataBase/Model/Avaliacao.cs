using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Model.DataBase.Model
{
    public class Avaliacao
    {
        public virtual int Id { get; set; }
        public virtual String Pontuacao { get; set; }
        public virtual String Comentario { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual Produto Produto { get; set; }
    }

    public class AvaliacaoMap : ClassMapping<Avaliacao>
    {

        public AvaliacaoMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Identity));
            Property(x => x.Pontuacao, m =>
            {
                m.NotNullable(true);
            });
            Property(x => x.Comentario);

            ManyToOne(x => x.Usuario, m =>
            {
                m.Column("idUsuario");


            });
            ManyToOne(x => x.Produto, m =>
            {
                m.Column("idProduto");


            });
        }

    }
}
