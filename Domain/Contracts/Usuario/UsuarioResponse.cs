using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ExercicioAPIStella.Domain.Contracts.Usuario
{
    public class UsuarioResponse
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nome precisa ser informado")]
        public string Nome { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Telefone precisa ser informado")]
        public string Telefone { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email precisa ser informado")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "CPF precisa ser informado")]
        public string CPF { get; set; }
    }
}
