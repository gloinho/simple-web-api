using System.Linq.Expressions;
using ExercicioAPIStella.Data.Context;
using ExercicioAPIStella.Data.Entities;
using ExercicioAPIStella.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExercicioAPIStella.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Usuario item)
        {
            await _context.Set<Usuario>().AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Usuario item)
        {
            _context.Entry<Usuario>(item).State = EntityState.Modified;
            // _context.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario> FindAsync(int id)
        {
            return await _context.Set<Usuario>().FirstOrDefaultAsync(u => u.UsuarioId == id);
        }

        public async Task<Usuario> FindAsync(string name)
        {
            return await _context.Set<Usuario>().FirstOrDefaultAsync(u => u.Nome == name);
        }

        public async Task<List<Usuario>> ListAsync()
        {
            return await _context.Set<Usuario>().ToListAsync();
        }

        public async Task RemoveAsync(Usuario item)
        {
            _context.Set<Usuario>().Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
