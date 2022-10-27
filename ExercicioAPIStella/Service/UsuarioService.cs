using ExercicioAPIStella.Domain.Contracts.Usuario;
using ExercicioAPIStella.Domain.Interfaces;
using CpfLibrary;
using ExercicioAPIStella.Domain.Entities;
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
            IsValidCpf(usuarioRequest.CPF);
            var novoUsuario = _mapper.Map<Usuario>(usuarioRequest);
            await _usuarioRepository.AddAsync(novoUsuario);
            return _mapper.Map<UsuarioResponse>(usuarioRequest);
        }

        public async Task<UsuarioResponse> EditarUsuario(int id, UsuarioRequest usuarioRequest)
        {
            var user = await IsValidUser(id, usuarioRequest);
            user = _mapper.Map(usuarioRequest, user);
            await _usuarioRepository.EditAsync(user);
            var usuarioEditado = await _usuarioRepository.FindAsync(id);
            return _mapper.Map<UsuarioResponse>(usuarioEditado);
        }

        public async Task ExcluirUsuarioPorId(int id)
        {
            var usuarioParaDeletar = await IsValidProcurarPeloId(id);
            await _usuarioRepository.RemoveAsync(usuarioParaDeletar);
        }

        public async Task<UsuarioResponse> GetUsuario(string name)
        {
            var user = await IsValidProcurarPeloNome(name);
            return _mapper.Map<UsuarioResponse>(user);
        }

        public async Task<UsuarioResponse> GetUsuarioPorId(int id)
        {
            var usuario = await IsValidProcurarPeloId(id);
            var usuarioResponse = _mapper.Map<UsuarioResponse>(usuario);
            return usuarioResponse;
        }

        public async Task<List<UsuarioResponse>> GetUsuarios()
        {
            var usuarios = await _usuarioRepository.ListAsync();
            var usuariosResponse = _mapper.Map<List<UsuarioResponse>>(usuarios);
            return usuariosResponse;
        }

        private async Task<Usuario> IsValidProcurarPeloNome(string name)
        {
            var user = await _usuarioRepository.FindAsync(name);
            IsNullUser(user);
            return user;
        }

        private async Task<Usuario> IsValidProcurarPeloId(int id)
        {
            var user = await _usuarioRepository.FindAsync(id);
            IsNullUser(user);
            return user;
        }

        private async Task<Usuario> IsValidUser(int id, UsuarioRequest usuarioRequest)
        {
            var user = await _usuarioRepository.FindAsync(id);
            IsNullUser(user);
            IsValidCpf(usuarioRequest.CPF);
            return user;
        }

        private void IsNullUser(Usuario user)
        {
            if (user == null)
            {
                throw new ArgumentException("Usuario não existe");
            }
        }

        private void IsValidCpf(string cpf)
        {
            if (!Cpf.Check(cpf))
            {
                throw new ArgumentException("CPF inválido.");
            }
        }
    }
}
