using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient; //acesso ao sqlserver
using System.Configuration; //connectionstring

namespace Projeto.Repository.Persistence
{
    public class Conexao
    {
        //atributos
        protected SqlConnection conexao;
        protected SqlCommand comando;
        protected SqlDataReader dataReader;
        protected SqlTransaction transacao;

        //método para abrir conexão com o banco
        protected void OpenConnection()
        {
            conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ControleCliente"].ConnectionString);
            conexao.Open(); //conectado!
        }

        //método para fechar conexão com o banco
        protected void CloseConnection()
        {
            conexao.Close(); //desconectado!
        }
    }
}
