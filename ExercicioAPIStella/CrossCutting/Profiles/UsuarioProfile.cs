using AutoMapper;
using ExercicioAPIStella.Domain.Entities;
using ExercicioAPIStella.Domain.Contracts.Usuario;

namespace ExercicioAPIStella.CrossCutting.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<UsuarioRequest, Usuario>();
            CreateMap<Usuario, UsuarioResponse>();
        }
    }
}
