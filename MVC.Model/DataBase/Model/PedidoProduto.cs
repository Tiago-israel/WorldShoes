using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Model.DataBase.Model
{
    public class PedidoProduto
    {
        public virtual int Id { get; set; }
        public virtual Pedido Pedido { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual int Quantidade { get; set; }
        public virtual decimal Total { get; set; }
    }

    public class PedidoProdutoMap : ClassMapping<PedidoProduto>
    {
        public PedidoProdutoMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Identity));
            Property(x => x.Quantidade);
            Property(x => x.Total, m => {
                m.Precision(10);
                m.Scale(2);
                m.NotNullable(true);
            });

            ManyToOne<Pedido>(x => x.Pedido, m => m.Column("idPedido"));
            ManyToOne<Produto>(x => x.Produto, m => m.Column("idProduto"));
        }
    }
}
