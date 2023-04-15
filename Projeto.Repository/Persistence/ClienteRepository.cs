using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient; //acesso ao sqlserver
using Projeto.Repository.Entities; //entidades
using Projeto.Repository.Entities.Types; //enums

namespace Projeto.Repository.Persistence
{
    public class ClienteRepository : Conexao
    {
        //método para inserir o cliente no banco de dados
        public void Insert(Cliente c)
        {
            OpenConnection(); //abrir conexão

            string query = "insert into Cliente (Nome, Email, Telefone, IdSexo, IdEstadoCivil) values (@Nome, @Email, @Telefone, @IdSexo, @IdEstadoCivil)";

            comando = new SqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@Nome", c.Nome);
            comando.Parameters.AddWithValue("@Email", c.Email);
            comando.Parameters.AddWithValue("@Telefone", c.Telefone);
            comando.Parameters.AddWithValue("@IdSexo", (int)c.Sexo);
            comando.Parameters.AddWithValue("@IdEstadoCivil", (int)c.EstadoCivil);
            comando.ExecuteNonQuery(); //executar

            CloseConnection(); //fechar conexão
        }

        //método para atualizar o cliente no banco de dados
        public void Update(Cliente c)
        {
            OpenConnection(); //abrir conexão

            string query = "update Cliente set Nome = @Nome , Email = @Email , Telefone = @Telefone , IdSexo = @IdSexo , IdEstadoCivil = @IdEstadoCivil where IdCliente = @IdCliente";

            comando = new SqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@Nome", c.Nome);
            comando.Parameters.AddWithValue("@Email", c.Email);
            comando.Parameters.AddWithValue("@Telefone", c.Telefone);
            comando.Parameters.AddWithValue("@IdSexo", (int)c.Sexo);
            comando.Parameters.AddWithValue("@IdEstadoCivil", (int)c.EstadoCivil);
            comando.Parameters.AddWithValue("@IdCliente", c.IdCliente);
            comando.ExecuteNonQuery(); //executar

            CloseConnection(); //fechar conexão
        }

        //método para excluir o cliente no banco de dados
        public void Delete(int idCliente)
        {
            OpenConnection();

            string query = "delete from Cliente where IdCliente = @IdCliente";

            comando = new SqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@IdCliente", idCliente);
            comando.ExecuteNonQuery(); //executar

            CloseConnection();
        }

        //método para consultar todos os clientes no banco de dados
        public List<Cliente> FindAll()
        {
            OpenConnection();

            string query = "select IdCliente, Nome, Email, Telefone, IdSexo, IdEstadoCivil from Cliente order by Nome, IdCliente";

            comando = new SqlCommand(query, conexao);
            dataReader = comando.ExecuteReader();

            List<Cliente> lista = new List<Cliente>();

            while (dataReader.Read()) //enquanto houver registros..
            {
                Cliente c = new Cliente(); //instanciando..
                c.IdCliente = Convert.ToInt32(dataReader["IdCliente"]);
                c.Nome = Convert.ToString(dataReader["Nome"]);
                c.Email = Convert.ToString(dataReader["Email"]);
                c.Telefone = Convert.ToString(dataReader["Telefone"]);
                c.Sexo = (Sexo)Convert.ToInt32(dataReader["IdSexo"]);
                c.EstadoCivil = (EstadoCivil)
                Convert.ToInt32(dataReader["IdEstadoCivil"]);

                lista.Add(c); //adicionar na lista..
            }

            CloseConnection();
            return lista;
        }

        //método para retornar 1 cliente pelo id..
        public Cliente FindById(int idCliente)
        {
            OpenConnection();

            string query = "select IdCliente, Nome, Email, Telefone, IdSexo, IdEstadoCivil from Cliente where IdCliente = @IdCliente";

            comando = new SqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@IdCliente", idCliente);
            dataReader = comando.ExecuteReader();

            Cliente c = null; //sem espaço de memória

            if (dataReader.Read()) //se algum registro foi encontrado..
            {
                c = new Cliente(); //instanciando..

                c.IdCliente = Convert.ToInt32(dataReader["IdCliente"]);
                c.Nome = Convert.ToString(dataReader["Nome"]);
                c.Email = Convert.ToString(dataReader["Email"]);
                c.Telefone = Convert.ToString(dataReader["Telefone"]);
                c.Sexo = (Sexo)Convert.ToInt32(dataReader["IdSexo"]);
                c.EstadoCivil = (EstadoCivil)Convert.ToInt32(dataReader["IdEstadoCivil"]);
            }

            CloseConnection();
            return c; //retornar o cliente
        }

        //método para retornar 1 cliente pelo id..
        public List<Cliente> FindByFiltro(Cliente c)
        {
            int controle;
            OpenConnection();
            List<Cliente> lista = new List<Cliente>();

            controle = 0;
            string query = "select IdCliente, Nome, Email, Telefone, IdSexo, IdEstadoCivil from Cliente where ";

            //IdCliente
            if (c.IdCliente > 0 )
            {
                controle = 1;
                query = query + "IdCliente = @IdCliente ";
            }

            //c.Nome
            if ((controle == 1) && (String.IsNullOrEmpty(c.Nome) == false))
            {
                controle = 1;
                query = query + "and Nome like '%" + c.Nome.ToString() + "%' ";
            }
            else if ((controle == 0) && (String.IsNullOrEmpty(c.Nome) == false))
            {
                controle = 1;
                query = query + "Nome like '%" + c.Nome.ToString() + "%' ";
            }

            //c.Email
            if ((controle == 1) && (String.IsNullOrEmpty(c.Email) == false))
            {
                controle = 1;
                query = query + "and Email like '%" + c.Email.ToString() + "%' ";
            }
            else if ((controle == 0) && (String.IsNullOrEmpty(c.Email) == false))
            {
                controle = 1;
                query = query + "Email like '%" + c.Email.ToString() + "%' ";
            }

            //c.Telefone
            if ((controle == 1) && (String.IsNullOrEmpty(c.Telefone) == false))
            {
                controle = 1;
                query = query + "and Telefone like '%" + c.Telefone.ToString() + "%' ";
            }
            else if ((controle == 0) && (String.IsNullOrEmpty(c.Telefone) == false))
            {
                controle = 1;
                query = query + "Telefone like '%" + c.Telefone.ToString() + "%' ";
            }

            //c.IdSexo
            if ((controle == 1) && (c.Sexo > 0))
            {
                controle = 1;
                query = query + "and IdSexo = @Sexo ";
            }
            else if ((controle == 0) && (c.Sexo > 0))
            {
                controle = 1;
                query = query + "IdSexo = @Sexo ";
            }

            //c.IdEstadoCivil
            if ((controle == 1) && (c.EstadoCivil > 0))
            {
                controle = 1;
                query = query + "and IdEstadoCivil = @EstadoCivil ";
            }
            else if ((controle == 0) && (c.EstadoCivil > 0))
            {
                controle = 1;
                query = query + "IdEstadoCivil = @EstadoCivil ";
            }

            query = query + "order by nome ";

            if (controle == 1)
            {
                comando = new SqlCommand(query, conexao);

                //IdCliente
                if (c.IdCliente > 0)
                {
                    comando.Parameters.AddWithValue("@IdCliente", c.IdCliente);
                }

                //c.IdSexo
                if (c.Sexo > 0)
                {
                    comando.Parameters.AddWithValue("@Sexo", c.Sexo);
                }

                //c.IdEstadoCivil
                if (c.EstadoCivil > 0)
                {
                    comando.Parameters.AddWithValue("@EstadoCivil", c.EstadoCivil);
                }

                dataReader = comando.ExecuteReader();

                while (dataReader.Read()) //enquanto houver registros..
                {
                    Cliente l = new Cliente(); //instanciando..
                    l.IdCliente = Convert.ToInt32(dataReader["IdCliente"]);
                    l.Nome = Convert.ToString(dataReader["Nome"]);
                    l.Email = Convert.ToString(dataReader["Email"]);
                    l.Telefone = Convert.ToString(dataReader["Telefone"]);
                    l.Sexo = (Sexo)Convert.ToInt32(dataReader["IdSexo"]);
                    l.EstadoCivil = (EstadoCivil)Convert.ToInt32(dataReader["IdEstadoCivil"]);
                    lista.Add(l); //adicionar na lista..
                }

                CloseConnection();
            }
            return lista;
        }
    }
}

