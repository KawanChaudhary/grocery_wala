using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GroceryWala.DataAccessLayer.Repository.Interface
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> All();
        Task<TEntity> GetById(Guid id);
        Task<TEntity> GetById(int id);
        Task<bool> Add(TEntity entity);
        bool Update(TEntity cartItem);
        Task<bool> Delete(int itemId);
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);

        TEntity FindFirst(Expression<Func<TEntity, bool>> condition);
    }
}
