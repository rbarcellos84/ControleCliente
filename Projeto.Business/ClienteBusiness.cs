using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Repository.Persistence;
using Projeto.Repository.Entities;

namespace Projeto.Business
{
    public class ClienteBusiness
    {
        //atributo..
        private ClienteRepository repository;
        //construtor..
        public ClienteBusiness()
        {
            //inicializar o atributo da classe ClienteRepository
            repository = new ClienteRepository();
        }
        //método para cadastrar o cliente
        public void Cadastrar(Cliente c)
        {
            repository.Insert(c);
        }
        //método para atualizar o cliente
        public void Atualizar(Cliente c)
        {
            repository.Update(c);
        }
        //método para excluir o cliente
        public void Excluir(int idCliente)
        {
            repository.Delete(idCliente);
        }
        //método para consultar todos os clientes
        public List<Cliente> ConsultarTodos()
        {
            return repository.FindAll();
        }
        //método para obter 1 cliente pelo id
        public Cliente ConsultarPorId(int idCliente)
        {
            return repository.FindById(idCliente);
        }
        //método para obter 1 cliente pelo id
        public List<Cliente> FiltrarConsulta(Cliente c)
        {
            return repository.FindByFiltro(c);
        }
    }
}

