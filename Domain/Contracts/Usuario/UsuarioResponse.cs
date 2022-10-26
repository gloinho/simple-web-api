using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExercicioAPIStella.Domain.Contracts.Usuario
{
    public class UsuarioResponse
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
    }
}
