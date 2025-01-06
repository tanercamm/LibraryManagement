using LibraryManagement.Data.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data.Repositories.Concrete
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly LibraryContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(LibraryContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();  // dinamik yapıda veri tabanı işlemleri yapabilmek için
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await SaveChangesAsync();
        }

        public Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        
    }
}
