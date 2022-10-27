using AutoMapper;
using ExercicioAPIStella.Data.Entities;
using ExercicioAPIStella.Domain.Contracts.Usuario;

namespace ExercicioAPIStella.Domain.Profiles
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
