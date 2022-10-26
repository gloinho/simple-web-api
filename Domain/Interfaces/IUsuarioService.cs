using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExercicioAPIStella.Domain.Contracts.Usuario;

namespace ExercicioAPIStella.Domain.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioResponse> CadastrarUsuario(UsuarioRequest usuarioRequest);
        Task<UsuarioResponse> GetUsuarioPorId(int id);
        Task<List<UsuarioResponse>> GetUsuarios();
        Task<UsuarioResponse> EditarUsuario(int id, UsuarioRequest usuarioRequest);
        Task ExcluirUsuarioPorId(int id);
    }
}
