using ExercicioAPIStella.Domain.Contracts.Usuario;
using ExercicioAPIStella.Domain.Interfaces;
using CpfLibrary;
using ExercicioAPIStella.Data.Entities;
using AutoMapper;
using System.Linq.Expressions;

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

        public async Task CadastrarUsuario(List<UsuarioRequest> usuariosRequests)
        {
            foreach (var u in usuariosRequests)
            {
                IsValid(u);
                var novoUsuario = _mapper.Map<Usuario>(u);
                await _usuarioRepository.AddAsync(novoUsuario);
            }
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
            var user = await IsValid(id, usuarioRequest);

            user = _mapper.Map(usuarioRequest, user);

            await _usuarioRepository.EditAsync(user);

            var usuarioEditado = await _usuarioRepository.FindAsync(id);

            return _mapper.Map<UsuarioResponse>(usuarioEditado);
        }

        public async Task ExcluirUsuarioPorId(int id)
        {
            var usuarioParaDeletar = await IsValid(id);
            await _usuarioRepository.RemoveAsync(usuarioParaDeletar);
        }

        public async Task<UsuarioResponse> GetUsuario(string name)
        {
            var user = await IsValid(name);
            return _mapper.Map<UsuarioResponse>(user);
        }

        public async Task<UsuarioResponse> GetUsuarioPorId(int id)
        {
            var usuario = await IsValid(id);
            var usuarioResponse = _mapper.Map<UsuarioResponse>(usuario);
            return usuarioResponse;
        }

        public async Task<List<UsuarioResponse>> GetUsuarios()
        {
            var usuarios = await _usuarioRepository.ListAsync();
            var usuariosResponse = _mapper.Map<List<UsuarioResponse>>(usuarios);
            return usuariosResponse;
        }

        private async Task<Usuario> IsValid(string name)
        {
            var user = await _usuarioRepository.FindAsync(name);
            if (user == null)
            {
                throw new ArgumentException("Usuario não existe");
            }
            return user;
        }

        private async Task<Usuario> IsValid(int id)
        {
            var user = await _usuarioRepository.FindAsync(id);
            if (user == null)
            {
                throw new ArgumentException("Usuario não existe");
            }
            return user;
        }

        private async Task<Usuario> IsValid(int id, UsuarioRequest usuarioRequest)
        {
            var user = await _usuarioRepository.FindAsync(id);
            if (user == null)
            {
                throw new ArgumentException("Usuario não existe");
            }

            if (!Cpf.Check(usuarioRequest.CPF))
            {
                throw new ArgumentException("CPF inválido.");
            }
            return user;
        }

        private void IsValid(UsuarioRequest usuarioRequest)
        {
            if (!Cpf.Check(usuarioRequest.CPF))
            {
                throw new ArgumentException("CPF inválido.");
            }
        }
    }
}
