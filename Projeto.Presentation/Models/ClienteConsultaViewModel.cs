using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Projeto.Repository.Entities.Types; //enums

namespace Projeto.Presentation.Models
{
    public class ClienteConsultaViewModel
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public Sexo Sexo { get; set; }
        public EstadoCivil EstadoCivil { get; set; }
    }
}
