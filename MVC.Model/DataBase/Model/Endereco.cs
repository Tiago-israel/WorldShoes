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
    public class Endereco
    {
        public virtual int Id { get; set; }
        [Required(ErrorMessage = "O campo Cep é obrigatório")]
        public virtual string Cep { get; set; }
        [Display(Name = "Número")]
        [Required(ErrorMessage = "O campo Número é obrigatório")]
        public virtual int Numero { get; set; }
        [Required(ErrorMessage = "O campo Rua é obrigatório")]
        public virtual String Rua { get; set; }
        [Required(ErrorMessage = "O campo Bairro é obrigatório")]
        public virtual String Bairro { get; set; }
        [Required(ErrorMessage = "O campo Cidade é obrigatório")]
        public virtual String Cidade { get; set; }
        [Required(ErrorMessage = "O campo Estado é obrigatório")]
        public virtual String Estado { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Fabricante Fabricante { get; set; }

        public virtual IList<Pedido> Pedidos { get; set; }

        public Endereco()
        {
            this.Pedidos = new List<Pedido>();
        }

        public override string ToString()
        {
            return this.Rua + ", " + this.Numero + " - " + this.Cidade + "(" + this.Estado + ")";
        }

    }

    public class EnderecoMap : ClassMapping<Endereco>
    {
        public EnderecoMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Identity));
            Property(x => x.Numero, m =>
            {
                m.NotNullable(true);
            });

            Property(x => x.Cep, m =>
            {
                m.NotNullable(true);
            });

            Property(x => x.Rua, m =>
            {
                m.NotNullable(true);
            });
            Property(x => x.Bairro, m =>
            {
                m.NotNullable(true);
            });
            Property(x => x.Cidade, m =>
            {
                m.NotNullable(true);
            });
            Property(x => x.Estado, m =>
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

          //  Bag<Pedido>(x => x.Pedidos, m =>
          //  {
          //      m.Cascade(Cascade.None);
          //      m.Key(k => k.Column("idEndereco"));
          //      m.Lazy(CollectionLazy.NoLazy);
          //      m.Inverse(true);
          //  },
          //r => r.OneToMany());


        }
    }
}
