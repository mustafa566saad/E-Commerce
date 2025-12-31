
using System.Linq.Expressions;

namespace E_commerce.infrastructer.Interfaces
{
    public interface IBaseRepo<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        void Add(T entity);
        void Update(T entity);
        Task DeleteAsync(int id);
        Task<bool> CheckExesistAsync(int id);
        Task<bool> CheckExesistAsync(Expression<Func<T, bool>> expression);
        public Task<T> GetByExpressionAsync(Expression<Func<T, bool>> expression);


    }
}
