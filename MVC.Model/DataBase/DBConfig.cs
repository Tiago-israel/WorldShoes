using MVC.Model.DataBase.Model;
using MVC.Model.DataBase.Repository;
using MySql.Data.MySqlClient;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Context;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MVC.Model.DataBase
{
    public class DBConfig
    {
        private static DBConfig _instance = null;
        private ISessionFactory _sessionFactory;

        public AvaliacaoRepository AvaliacaoRepository { get; set; }
        public CategoriaRepository CategoriaRepository { get; set; }
        public CorRepository CorRepository { get; set; }
        public EnderecoRepository EnderecoRepository { get; set; }
        public FabricanteRepository FabricanteRepository { get; set; }
        public FotoProdutoRepository FotoProdutoRepository { get; set; }
        public GeneroRepository GeneroRepository { get; set; }
        public PedidoRepository PedidoRepository { get; set; }
        public ProdutoRepository ProdutoRepository { get; set; }
        public TelefoneRepository TelefoneRepository { get; set; }
        public UsuarioRepository UsuarioRepository { get; set; }
        public AdministradorRepository AdministradorRepository { get; set; }
        public PreferenciaRepository PreferenciaRepository { get; set; }
        public HistoricoRepository HistoricoRepository { get; set; }
        public PedidoProdutoRepository PedidoProdutoRepository { get; set;}

        private DBConfig()
        {
            Conectar();
            this.AvaliacaoRepository = new AvaliacaoRepository(Session);
            this.CategoriaRepository = new CategoriaRepository(Session);
            this.CorRepository = new CorRepository(Session);
            this.EnderecoRepository = new EnderecoRepository(Session);
            this.FabricanteRepository = new FabricanteRepository(Session);
            this.FotoProdutoRepository = new FotoProdutoRepository(Session);
            this.GeneroRepository = new GeneroRepository(Session);
            this.PedidoRepository = new PedidoRepository(Session);
            this.ProdutoRepository = new ProdutoRepository(Session);
            this.TelefoneRepository = new TelefoneRepository(Session);
            this.UsuarioRepository = new UsuarioRepository(Session);
            this.AdministradorRepository = new AdministradorRepository(Session);
            this.PreferenciaRepository = new PreferenciaRepository(Session);
            this.HistoricoRepository = new HistoricoRepository(Session);
            this.PedidoProdutoRepository = new PedidoProdutoRepository(Session);
        }

        public static DBConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DBConfig();
                }

                return _instance;
            }
        }
        //pwd=!@#$1234
        private void Conectar()
        {
            try
            {
                var stringConexao = "Persist Security Info=True;server=localhost; port=3306; database=worldshoes; uid=root; pwd=";
                var mysql = new MySqlConnection(stringConexao);
                try
                {
                    mysql.Open();
                }
                catch
                {
                    //!@#$1234
                    CriarSchemaBanco("localhost", "3306", "worldshoes", "", "root");
                }
                finally
                {
                    mysql.Close();
                }
                ConectarNHibernate(stringConexao);

            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possivel conectar ao banco de dados.", ex);

            }
        }

        private void ConectarNHibernate(String stringConexao)
        {
            try
            {
                var config = new Configuration();
                // configurar a conexão com o banco
                config.DataBaseIntegration(db =>
                {
                    //dialeto do sql
                    db.Dialect<NHibernate.Dialect.MySQLDialect>();
                    //scring de conexão
                    db.ConnectionString = stringConexao;
                    //drive de conexão
                    db.Driver<NHibernate.Driver.MySqlDataDriver>();
                    // provedor do sql
                    db.ConnectionProvider<NHibernate.Connection.DriverConnectionProvider>();
                    //jeito de criação do banco de dados
                    db.SchemaAction = SchemaAutoAction.Update;
                });
                var maps = this.Mapeamento();
                config.AddMapping(maps);

                if (HttpContext.Current == null)
                {
                    config.CurrentSessionContext<ThreadStaticSessionContext>();
                }
                else
                {
                    config.CurrentSessionContext<WebSessionContext>();

                }
                this._sessionFactory = config.BuildSessionFactory();
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possivel criar o NHbernate.", ex);
            }
        }


        private HbmMapping Mapeamento()
        {
            try
            {
                var modelMapper = new ModelMapper();
                modelMapper.AddMappings(
                    Assembly.GetAssembly(typeof(UsuarioMap)).GetTypes()
                    );
                return modelMapper.CompileMappingForAllExplicitlyAddedEntities();
            }
            catch (Exception ex)
            {

                throw new Exception("Não foi possivel criar o NHbernate.", ex);
            }
        }

        private void CriarSchemaBanco(string server, string port, string dbName, string psw, string user)
        {
            try
            {
                var stringConexao = "server=" + server + ";user=" + user + ";port=" + port + ";password=" + psw + ";";
                var mySql = new MySqlConnection(stringConexao);
                var cmd = mySql.CreateCommand();
                mySql.Open();
                cmd.CommandText = "CREATE DATABASE IF NOT EXISTS `" + dbName + "`;";
                cmd.ExecuteNonQuery();
                mySql.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Não criar schema de bancos de dados.", ex);
            }
        }

        private ISession Session
        {
            get
            {
                try
                {
                    if (CurrentSessionContext.HasBind(_sessionFactory))
                        return _sessionFactory.GetCurrentSession();

                    var session = _sessionFactory.OpenSession();
                    CurrentSessionContext.Bind(session);
                    return session;
                }
                catch (Exception ex)
                {
                    throw new Exception("Não foi possivel criar a sessao", ex);
                }
            }
        }
    }
}