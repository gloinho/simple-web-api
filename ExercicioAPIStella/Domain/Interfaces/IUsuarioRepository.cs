using ExercicioAPIStella.Data.Entities;
using System.Linq.Expressions;

namespace ExercicioAPIStella.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task AddAsync(Usuario item);
        Task RemoveAsync(Usuario item);
        Task EditAsync(Usuario item);
        Task<Usuario> FindAsync(int id);
        Task<Usuario> FindAsync(string name);
        Task<List<Usuario>> ListAsync();
    }
}
