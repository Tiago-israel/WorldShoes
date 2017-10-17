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
    public class Genero
    {
        public virtual int Id { get; set; }
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public virtual String Nome { get; set; }

        public virtual IList<Produto> Produtos { get; set; }

        public Genero()
        {
            this.Produtos = new List<Produto>();
        }


    }

    public class GeneroMap : ClassMapping<Genero>
    {
        public GeneroMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Identity));

            Property(x => x.Nome, m => {
                m.NotNullable(true);
                m.Unique(true);
            });

            Bag<Produto>(x => x.Produtos, m =>
            {
                m.Cascade(Cascade.None);
                m.Key(k => k.Column("idGenero"));
                m.Lazy(CollectionLazy.NoLazy);
                m.Inverse(true);
            },
          r => r.OneToMany());

        }
    }
}
