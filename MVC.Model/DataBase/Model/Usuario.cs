using NHibernate;
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
    public class Usuario
    {
        public virtual int Id { get; set; }
        [Required(ErrorMessage ="O Campo Nome é obrigatório")]
        public virtual String Nome { get; set; }
        public virtual String Sobrenome { get; set; }
        [Required(ErrorMessage = "O Campo Email é obrigatório")]
        public virtual String Email { get; set; }
        [Required(ErrorMessage = "O Campo Senha é obrigatório")]
        public virtual String Senha { get; set; }
        public virtual String Cpf { get; set; }
        public virtual DateTime DataNascimento { get; set; }
        public virtual String Sexo { get; set; }

        public virtual IList<Telefone> Telefones { get; set; }
        public virtual IList<Pedido> Pedidos { get; set; }
        public virtual IList<Avaliacao> Avaliacoes { get; set; }
        public virtual IList<Endereco> Enderecos { get; set; }
        public virtual IList<HistoricoBusca> HistoricoBuscas { get; set; }

        public Usuario()
        {
            this.Telefones = new List<Telefone>();
            this.Pedidos = new List<Pedido>();
            this.Avaliacoes = new List<Avaliacao>();
            this.Enderecos = new List<Endereco>();
            this.HistoricoBuscas = new List<HistoricoBusca>();
        }
    }

    public class UsuarioMap : ClassMapping<Usuario>
    {
        public UsuarioMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Identity));

            Property(x => x.Nome, m =>
            {
                m.NotNullable(true);
            });
            Property(x => x.Sobrenome);
            Property(x => x.Email, m =>
            {
                m.Unique(true);
                m.NotNullable(true);
            });
            Property(x => x.Senha, m =>
            {
                m.NotNullable(true);
            });
            Property(x => x.Cpf, m =>
            {
                m.Unique(true);
            });
            Property(x => x.Sexo);
            Property(x => x.DataNascimento, m =>
            {
                m.Type(NHibernateUtil.Date);
            });

            Bag<Telefone>(x => x.Telefones, m =>
            {
                m.Cascade(Cascade.All);
                m.Key(k => k.Column("idUsuario"));
                m.Lazy(CollectionLazy.NoLazy);
                m.Inverse(true);
            },
            r => r.OneToMany());

            Bag<Pedido>(x => x.Pedidos, m =>
            {
                m.Cascade(Cascade.All);
                m.Key(k => k.Column("idUsuario"));
                m.Lazy(CollectionLazy.NoLazy);
                m.Inverse(true);
            },
            r => r.OneToMany());

            Bag<Avaliacao>(x => x.Avaliacoes, m =>
            {
                m.Cascade(Cascade.All);
                m.Key(k => k.Column("idUsuario"));
                m.Lazy(CollectionLazy.NoLazy);
                m.Inverse(true);
            },
            r => r.OneToMany());


            Bag<HistoricoBusca>(x => x.HistoricoBuscas, m =>
            {
                m.Cascade(Cascade.All);
                m.Key(k => k.Column("idUsuario"));
                m.Lazy(CollectionLazy.NoLazy);
                m.Inverse(true);

            }, r => r.OneToMany());

            Bag<Endereco>(x => x.Enderecos, m =>
            {
                m.Cascade(Cascade.All);
                m.Key(k => k.Column("idUsuario"));
                m.Lazy(CollectionLazy.NoLazy);
                m.Inverse(true);
            },
            r => r.OneToMany());


        }
    }
}
