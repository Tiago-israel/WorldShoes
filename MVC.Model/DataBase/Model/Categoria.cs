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
    public class Categoria
    {
        public virtual int Id { get; set; }
        [Required(ErrorMessage = "O Campo nome é obrigatório")]
        public virtual String Nome { get; set; }

        public virtual IList<Produto> Produtos { get; set; }

        public Categoria()
        {
            this.Produtos = new List<Produto>();
        }

    }

    public class CategoriaMap : ClassMapping<Categoria>
    {
        public CategoriaMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Identity));

            Property(x => x.Nome, m =>
            {
                m.NotNullable(true);
            });

            Bag<Produto>(x => x.Produtos, m =>
            {
                m.Cascade(Cascade.None);
                m.Key(k => k.Column("idCategoria"));
                m.Lazy(CollectionLazy.NoLazy);
                m.Inverse(true);
            },
            r => r.OneToMany());

        }
    }
}
