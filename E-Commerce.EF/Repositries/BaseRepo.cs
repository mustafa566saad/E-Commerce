using E_commerce.infrastructer.Interfaces;
using E_Commerce.EF.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace E_Commerce.EF.Repositries
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        private readonly AppDbContext _context;

        public BaseRepo(AppDbContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        => _context.Add(entity);

        public async Task<bool> CheckExesistAsync(int id)
        => await _context.Set<T>().FindAsync(id) != null;

        public async Task<bool> CheckExesistAsync(Expression<Func<T, bool>> expression)
        => await _context.Set<T>().AnyAsync(expression);

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            _context.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        => await _context.Set<T>().AsNoTracking().ToListAsync();

        public async Task<T> GetByIdAsync(int id)
        => await _context.Set<T>().FindAsync(id);

        public async Task<T> GetByExpressionAsync(Expression<Func<T, bool>> expression)
        => await _context.Set<T>().FirstOrDefaultAsync(expression);

        public void Update(T entity)
        => _context.Set<T>().Update(entity).State = EntityState.Modified;

       
    }
}
