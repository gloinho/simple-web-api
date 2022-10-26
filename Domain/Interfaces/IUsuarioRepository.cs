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
        Task<Usuario> FindAsync(Expression<Func<Usuario, bool>> expression);
        Task<List<Usuario>> ListAsync();
        Task<List<Usuario>> ListAsync(Expression<Func<Usuario, bool>> expression);
    }
}
