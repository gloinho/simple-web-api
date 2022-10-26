using ExercicioAPIStella.Domain.Contracts.Usuario;
using ExercicioAPIStella.Domain.Interfaces;
using CpfLibrary;
using ExercicioAPIStella.Data.Entities;
using AutoMapper;

namespace ExercicioAPIStella.Service
{
    public class UsuarioService : IUsuarioService
    {
        public readonly IUsuarioRepository _usuarioRepository;
        public readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<UsuarioResponse> CadastrarUsuario(UsuarioRequest usuarioRequest)
        {
            IsValid(usuarioRequest);
            // PESQUISAR AUTOMAPPER!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            // var newUser = new Usuario()
            // {
            //     UsuarioId = usuarioRequest.UsuarioId,
            //     CPF = usuarioRequest.CPF,
            //     Email = usuarioRequest.Email,
            //     Nome = usuarioRequest.Nome,
            //     Senha = usuarioRequest.Senha,
            //     Telefone = usuarioRequest.Telefone
            // };
            var novoUsuario = _mapper.Map<Usuario>(usuarioRequest);
            await _usuarioRepository.AddAsync(novoUsuario);
            return _mapper.Map<UsuarioResponse>(usuarioRequest);
        }

        public async Task<UsuarioResponse> EditarUsuario(int id, UsuarioRequest usuarioRequest)
        {
            await IsValid(id, usuarioRequest);
            var usuarioParaEditar = _mapper.Map<Usuario>(usuarioRequest);
            await _usuarioRepository.EditAsync(usuarioParaEditar);
            var usuarioEditado = _usuarioRepository.FindAsync(id);
            return _mapper.Map<UsuarioResponse>(usuarioEditado);
        }

        public async Task ExcluirUsuarioPorId(int id)
        {
            await IsValid(id);
            var usuarioParaDeletar = await _usuarioRepository.FindAsync(id);
            await _usuarioRepository.RemoveAsync(usuarioParaDeletar);
        }

        public async Task<UsuarioResponse> GetUsuarioPorId(int id)
        {
            await IsValid(id);
            var usuario = await _usuarioRepository.FindAsync(id);
            var usuarioResponse = _mapper.Map<UsuarioResponse>(usuario);
            return usuarioResponse;
        }

        public async Task<List<UsuarioResponse>> GetUsuarios()
        {
            var usuarios = await _usuarioRepository.ListAsync();
            var usuariosResponse = _mapper.Map<List<UsuarioResponse>>(usuarios);
            return usuariosResponse;
        }

        private async Task IsValid(int id)
        {
            var user = await _usuarioRepository.FindAsync(id);
            if (user == null)
            {
                throw new ArgumentException("Usuario não existe");
            }
        }

        private async Task IsValid(int id, UsuarioRequest usuarioRequest)
        {
            var user = await _usuarioRepository.FindAsync(id);
            if (user == null)
            {
                throw new ArgumentException("Usuario não existe");
            }

            if (string.IsNullOrEmpty(usuarioRequest.Nome))
            {
                throw new ArgumentException("Nome precisa ser informado.");
            }
            if (!Cpf.Check(usuarioRequest.CPF))
            {
                throw new ArgumentException("CPF inválido.");
            }
        }

        private void IsValid(UsuarioRequest usuarioRequest)
        {
            if (string.IsNullOrEmpty(usuarioRequest.Nome))
            {
                throw new ArgumentException("Nome precisa ser informado.");
            }
            if (!Cpf.Check(usuarioRequest.CPF))
            {
                throw new ArgumentException("CPF inválido.");
            }
        }
    }
}
