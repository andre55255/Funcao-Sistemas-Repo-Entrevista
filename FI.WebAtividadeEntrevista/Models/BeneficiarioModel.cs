using FI.AtividadeEntrevista.BLL.CustomValidationsAnotations;
using System.ComponentModel.DataAnnotations;

namespace FI.WebAtividadeEntrevista.Models
{
    public class BeneficiarioModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Cpf é obrigatório")]
        [CPFValidation(ErrorMessage = "Cpf é inválido")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(50, ErrorMessage = "Nome pode ter no máximo 50 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Código do cliente é obrigatório")]
        public long IdCliente { get; set; }
    }
}