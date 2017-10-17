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
    public class Produto : IComparable<Produto>
    {
        public virtual int Id { get; set; }
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public virtual String Nome { get; set; }
        [Required(ErrorMessage = "O campo Descrição é obrigatório")]
        [Display(Name = "Descrição")]
        public virtual String Descricao { get; set; }
        [Required(ErrorMessage = "O campo Valor é obrigatório")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        public virtual decimal Valor { get; set; }
        [Required(ErrorMessage = "O campo Estoque é obrigatório")]
        public virtual int Estoque { get; set; }
        [Required(ErrorMessage = "O campo Tamanho é obrigatório")]
        public virtual int Tamanho { get; set; }
        [Required(ErrorMessage = "O campo Peso é obrigatório")]
        public virtual double Peso { get; set; }
        [Required(ErrorMessage = "O campo Categoria é obrigatório")]
        public virtual Categoria Categoria { get; set; }
        
        [Required(ErrorMessage = "O campo Gênero é obrigatório")]
        [Display(Name = "Gênero")]
        public virtual Genero Genero { get; set; }
        [Required(ErrorMessage = "O campo Cor é obrigatório")]
        public virtual Cor Cor { get; set; }
        [Required(ErrorMessage = "O campo Fabricante é obrigatório")]
        public virtual Fabricante Fabricante { get; set; }

        public virtual IList<Avaliacao> Avaliacoes { get; set; }
        public virtual IList<FotoProduto> FotosProduto { get; set; }
        public virtual IList<PedidoProduto> PedidoProdutos { get; set; }

        public Produto()
        {
            this.Avaliacoes = new List<Avaliacao>();
            this.FotosProduto = new List<FotoProduto>();
            this.PedidoProdutos = new List<PedidoProduto>();
        }

        public virtual int CompareTo(Produto other)
        {
            return this.Nome.CompareTo(other.Nome);
        }
    }

    public class ProdutoMap : ClassMapping<Produto>
    {
        public ProdutoMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Identity));

            Property(x => x.Nome, m =>
            {
                m.NotNullable(true);
            });
            Property(x => x.Descricao, m =>
            {
                m.NotNullable(true);
            });
            Property(x => x.Valor, m =>
            {
                m.Precision(10);
                m.Scale(2);
                m.NotNullable(true);
            });
            Property(x => x.Estoque, m =>
            {
                m.NotNullable(true);
            });
            Property(x => x.Tamanho, m =>
            {
                m.NotNullable(true);
            });
            Property(x => x.Peso, m =>
            {
                m.NotNullable(true);
            });

            ManyToOne(x => x.Categoria, m =>
            {
                m.Column("idCategoria");

            });

            ManyToOne(x => x.Genero, m =>
            {
                m.Column("idGenero");

            });

            ManyToOne(x => x.Cor, m =>
            {
                m.Column("idCor");

            });

            ManyToOne(x => x.Fabricante, m =>
            {
                m.Column("idFabricante");

            });

            Bag<Avaliacao>(x => x.Avaliacoes, m =>
            {
                m.Cascade(Cascade.All);
                m.Key(k => k.Column("idProduto"));
                m.Lazy(CollectionLazy.NoLazy);
                m.Inverse(true);
            },
          r => r.OneToMany());

            Bag<FotoProduto>(x => x.FotosProduto, m =>
            {
                m.Cascade(Cascade.All);
                m.Key(k => k.Column("idProduto"));
                m.Lazy(CollectionLazy.NoLazy);
                m.Inverse(true);
            },
         r => r.OneToMany());

            Bag<PedidoProduto>(x => x.PedidoProdutos, m =>
            {
                m.Cascade(Cascade.All);
                m.Inverse(true);
                m.Lazy(CollectionLazy.NoLazy);
                m.Key(k => k.Column("idProduto"));
            },
            o => o.OneToMany());

        }
    }
}
