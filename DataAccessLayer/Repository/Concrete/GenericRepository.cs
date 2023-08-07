using GroceryWala.DataAccessLayer.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GroceryWala.DataAccessLayer.Repository.Concrete
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected GroceryWalaContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(GroceryWalaContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<bool> Add(TEntity entity)
        {
            try
            {

                var res = await dbSet.AddAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }


        public virtual async Task<IEnumerable<TEntity>> All()
        {
                return await dbSet.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }

        public TEntity FindFirst(Expression<Func<TEntity, bool>> condition)
        {
            return dbSet.FirstOrDefault(condition.Compile());
        }

        public bool Update(TEntity cartItem)
        {
            dbSet.Update(cartItem);
            return true;
        }

        public virtual async Task<bool> Delete(int itemId)
        {
            var item = await dbSet.FindAsync(itemId);
            dbSet.Remove(item);
            return true;
        }
    }
}
