using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Model.DataBase.Model
{
    public class Pedido
    {
        public virtual int Id { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual DateTime DataPedido { get; set; }
        public virtual Endereco LocalEntrega { get; set; }
        public virtual IList<PedidoProduto> PedidoProduto { get; set; }
        
        public Pedido()
        {
            this.PedidoProduto = new List<PedidoProduto>();
        }

    }

    public class PedidoMap : ClassMapping<Pedido>
    {

        public PedidoMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Identity));

            Property(x => x.DataPedido, m =>
            {
                m.NotNullable(true);
            });
            
            ManyToOne(x => x.Usuario, m =>
            {
                m.NotNullable(true);
                m.Column("idUsuario");
            });

            ManyToOne(x => x.LocalEntrega, m =>
            {
                m.Column("idLocalEntrega");

            });

            Bag<PedidoProduto>(x => x.PedidoProduto, m =>
            {
                m.Cascade(Cascade.All);
                m.Inverse(true);
                m.Lazy(CollectionLazy.NoLazy);
                m.Key(k => k.Column("idPedido"));
            },
            o => o.OneToMany());

        }
    }
}
