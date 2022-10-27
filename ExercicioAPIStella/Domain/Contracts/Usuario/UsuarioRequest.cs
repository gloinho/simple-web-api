using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ExercicioAPIStella.Domain.Contracts.Usuario
{
    public class UsuarioRequest : UsuarioResponse
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Senha deve ser informada.")]
        [StringLength(
            100,
            MinimumLength = 6,
            ErrorMessage = "Senha precisa conter no minimo 6 caracteres."
        )]
        public string Senha { get; set; }
    }
}
