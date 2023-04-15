using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Projeto.Repository.Entities.Types;

namespace Projeto.Presentation.Models
{
    //No formulario de cdastro devemos colocar a seguinte ferencia a ClienteCadastroViewModel
    //@model Projeto.Presentation.Models.ClienteCadastroViewModel

    public class ClienteCadastroViewModel
    {
        [MinLength(6, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(50, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o nome do cliente.")]
        public string Nome { get; set; }

        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de email válido.")]
        [Required(ErrorMessage = "Por favor, informe o email do cliente.")]
        public string Email { get; set; }

        [MinLength(11, ErrorMessage = "Por favor, informe o telefone com o DD e o telefone, somente números.")]
        [MaxLength(11, ErrorMessage = "Por favor, informe o telefone com o DD e o telefone, somente números")]
        [Range(0,99999999999, ErrorMessage = "Por favor, informe o telefone com 11 digitos numéricos")]
        [Required(ErrorMessage = "Por favor, informe o telefone do cliente.")]
        public string Telefone { get; set; }

        [Range(1, 2, ErrorMessage = "Por favor, selecione o sexo do cliente.")]
        [Required(ErrorMessage = "Por favor, selecione o sexo do cliente.")]
        public Sexo Sexo { get; set; }

        [Range(1, 4, ErrorMessage = "Por favor, selecione o estado civil do cliente.")]
        [Required(ErrorMessage = "Por favor, selecione o estado civil do cliente.")]
        public EstadoCivil EstadoCivil { get; set; }
    }
}

