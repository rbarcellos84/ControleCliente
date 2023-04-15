using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Repository.Entities.Types;

namespace Projeto.Repository.Entities
{
    public class Cliente
    {
        //propriedades [prop] + 2x[tab]
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public Sexo Sexo { get; set; }
        public EstadoCivil EstadoCivil { get; set; }

        //construtor default [ctor] + 2x[tab]
        public Cliente()
        {
            //vazio
        }

        //sobrescarga de métodos (overloading)
        public Cliente(int idCliente, string nome, string email, string telefone, Sexo sexo, EstadoCivil estadoCivil)
        {
            IdCliente = idCliente;
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Sexo = sexo;
            EstadoCivil = estadoCivil;
        }

        //sobrescrita do método ToString()
        public override string ToString()
        {
            return $"Id: {IdCliente}, Nome: {Nome}, Email: {Email}, Telefone: {Telefone}, Sexo: { Sexo}, Estado Civil: { EstadoCivil}";
        }
    }
}

