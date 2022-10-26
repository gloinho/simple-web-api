using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExercicioAPIStella.Domain.Contracts.Usuario
{
    public class UsuarioRequest : UsuarioResponse
    {
        public string Senha { get; set; }
    }
}
