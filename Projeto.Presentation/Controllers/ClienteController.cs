using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projeto.Presentation.Models;
using Projeto.Business; //classes de negócio
using Projeto.Repository.Entities; //entidades

namespace Projeto.Presentation.Controllers
{
    public class ClienteController : Controller
    {
        //atributo para a classe de negocio do cliente..
        private ClienteBusiness business;
        //construtor..
        public ClienteController()
        {
            //inicializar o atributo ClienteBusiness..
            business = new ClienteBusiness();
        }
        // GET: Cliente/Filtrar
        public ActionResult Filtrar()
        {
            return View();
        }
        // GET: Cliente/Cadastro
        public ActionResult Cadastro()
        {
            return View();
        }
        // GET: Cliente/Consulta
        public ActionResult Consulta()
        {
            //executar a consulta de clientes
            //var lista = ObterConsultaDeClientes();

            //enviando  a lista para a página..
            //return View(lista);
            return View();
        }
        // GET: Cliente/Listar
        public ActionResult Listar()
        {
            //executar a consulta de clientes
            var lista = ObterConsultaDeClientes();

            //enviando  a lista para a página..
            return View(lista);
        }
        //GET: Cliente/Exclusao?id=1
        public ActionResult Exclusao(int id)
        {
            var model = new ClienteExclusaoViewModel();

            try
            {
                //buscar o cliente pelo id..
                Cliente c = business.ConsultarPorId(id);

                model.IdCliente = c.IdCliente;
                model.Nome = c.Nome;
                model.Email = c.Email;
                model.Telefone = c.Telefone;
                model.Sexo = c.Sexo;
                model.EstadoCivil = c.EstadoCivil;
            }
            catch (Exception e)
            {
                ViewBag.Mensagem = "Erro: " + e.Message;
            }

            return View(model);
        }
        // GET: Cliente/Selecao
        public ActionResult Selecao()
        {
            var list = new List<Cliente>();
            return View(list);
        }
        // GET: Cliente/Selecao
        public ActionResult Tecnologia()
        {
            return View();
        }
        //GET: método para acessar a camada de negocio e retornar a consulta de clientes selecionado
        private List<ClienteConsultaFiltrarViewModel> SelecionarFiltro(Cliente filtro)
        {
            //declarando e inicializando uma lista
            var lista = new List<ClienteConsultaFiltrarViewModel>();

            //acessando a camada business para consultar os clientes
            foreach (Cliente c in business.FiltrarConsulta(filtro))
            {
                var model = new ClienteConsultaFiltrarViewModel();
                model.IdCliente = c.IdCliente;
                model.Nome = c.Nome;
                model.Email = c.Email;
                model.Telefone = c.Telefone;
                model.Sexo = c.Sexo;
                model.EstadoCivil = c.EstadoCivil;

                lista.Add(model); //adicionar na lista
            }

            //retornando a lista
            return lista;
        }
        //GET: método para acessar a camada de negocio e retornar a consulta de clientes -  Cliente/Edicao?id=1
        public ActionResult Edicao(int id)
        {
            var model = new ClienteEdicaoViewModel();

            try
            {
                //buscar o cliente pelo id..
                Cliente c = business.ConsultarPorId(id);

                model.IdCliente = c.IdCliente;
                model.Nome = c.Nome;
                model.Email = c.Email;
                model.Telefone = c.Telefone;
                model.Sexo = c.Sexo;
                model.EstadoCivil = c.EstadoCivil;
            }
            catch (Exception e)
            {
                ViewBag.Mensagem = "Erro: " + e.Message;
            }

            return View(model);
        }
        [HttpPost] //método recebe os dados enviados por FormMethod.POST
        public ActionResult CadastrarCliente(ClienteCadastroViewModel model)
        {
            //verificar se os dados recebidos 
            //passaram nas regras de validação
            if (ModelState.IsValid)
            {
                try
                {
                    //transferindo os dados da viewmodel para a entidade
                    Cliente c = new Cliente();
                    c.Nome = model.Nome;
                    c.Email = model.Email;
                    c.Telefone = model.Telefone;
                    c.Sexo = model.Sexo;
                    c.EstadoCivil = model.EstadoCivil;

                    business.Cadastrar(c);

                    ViewBag.Mensagem = $"Cliente {c.Nome}, cadastrado com sucesso.";
                    ModelState.Clear(); //limpando os campos do formulário
                }
                catch (Exception e)
                {
                    ViewBag.Mensagem = "Ocorreu um erro: " + e.Message;
                }
            }

            //voltar para a página..
            return View("Cadastro");
        }
        [HttpPost] //método recebe os dados enviados por FormMethod.POST
        public ActionResult ExcluirCliente(ClienteExclusaoViewModel model)
        {
            //verificar se os dados recebidos 
            //passaram nas regras de validação
            if (ModelState.IsValid)
            {
                try
                {
                    business.Excluir(model.IdCliente);
                    ViewBag.Mensagem = "Cliente excluído com sucesso.";
                }
                catch (Exception e)
                {
                    ViewBag.Mensagem = "Ocorreu um erro: " + e.Message;
                }
            }

            //voltar para a página..
            var lista = ObterConsultaDeClientes();
            return View("Consulta", lista);
        }
        [HttpPost] //método recebe os dados enviados por FormMethod.POST
        public ActionResult FiltrarClientes(ClienteConsultaFiltrarViewModel model)
        {
            if (validarFiltro(model))
            {
                Cliente c = new Cliente();
                c.IdCliente = model.IdCliente;
                c.Nome = model.Nome;
                c.Email = model.Email;
                c.Telefone = model.Telefone;
                c.Sexo = model.Sexo;
                c.EstadoCivil = model.EstadoCivil;

                var lista = SelecionarFiltro(c);

                if (lista.Count == 0)
                {
                    ViewBag.Mensagem = "Seleção de clientes não encontrado.";
                }

                return View("Selecao", lista);
            }
            else
            {
                ViewBag.Mensagem = "Informe uma opção de filtro.";
                return View("Filtrar");
            }
        }
        [HttpPost] //método recebe os dados enviados por FormMethod.POST
        public ActionResult AtualizarCliente(ClienteEdicaoViewModel model)
        {
            //verificar se os dados recebidos 
            //passaram nas regras de validação
            if (ModelState.IsValid)
            {
                try
                {
                    //transferindo os dados da viewmodel para a entidade
                    Cliente c = new Cliente();
                    c.Nome = model.Nome;
                    c.Email = model.Email;
                    c.Telefone = model.Telefone;
                    c.Sexo = model.Sexo;
                    c.EstadoCivil = model.EstadoCivil;
                    c.IdCliente = model.IdCliente;

                    business.Atualizar(c);

                    ViewBag.Mensagem = $"Cliente {c.Nome}, atualizado com sucesso.";
                }
                catch (Exception e)
                {
                    ViewBag.Mensagem = "Ocorreu um erro: " + e.Message;
                }
            }

            //voltar para a página..
            return View("Edicao");
        }
        //Deve obedecer o nome das views
        [HttpPost] //método para acessar a camada de negocio e retornar a consulta de clientes
        private List<ClienteConsultaListarViewModel> ObterConsultaDeClientes()
        {
            //declarando e inicializando uma lista
            var lista = new List<ClienteConsultaListarViewModel>();

            //acessando a camada business para consultar os clientes
            foreach (Cliente c in business.ConsultarTodos())
            {
                var model = new ClienteConsultaListarViewModel();
                model.IdCliente = c.IdCliente;
                model.Nome = c.Nome;
                model.Email = c.Email;
                model.Telefone = c.Telefone;
                model.Sexo = c.Sexo;
                model.EstadoCivil = c.EstadoCivil;

                lista.Add(model); //adicionar na lista
            }

            //retornando a lista
            return lista;
        }
        //metodo para validação da dos objeto cliente
        private Boolean validarFiltro(ClienteConsultaFiltrarViewModel model)
        {
            if (model.IdCliente > 0)
            {
                return (true);
            }

            //c.Nome
            if (String.IsNullOrEmpty(model.Nome) != false)
            {
                return (true);
            }

            //c.Email
            if (String.IsNullOrEmpty(model.Email) != false)
            {
                return (true);
            }

            //c.Telefone
            if (String.IsNullOrEmpty(model.Telefone) != false)
            {
                return (true);
            }

            //c.IdSexo
            if (model.Sexo > 0)
            {
                return (true);
            }

            //c.IdEstadoCivil
            if (model.EstadoCivil > 0)
            {
                return (true);
            }

            return (false);
        }
    }
}