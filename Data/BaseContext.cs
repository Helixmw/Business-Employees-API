using Employees_API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Employees_API.Exceptions;

namespace Employees_API.Data
{
    public class BaseContext<T> : IBaseContext<T> where T : class, IIdentification
    {

        private readonly DbSet<T> DbSet;

        private readonly ApplicationDBContext _dbContext;
        public BaseContext(DbSet<T> DbSet)
        {
            this.DbSet = DbSet;
        }
        public async Task AddAsync(T Value)
        {
            await DbSet.AddAsync(Value);
        }

        public void Delete(T Value)
        {
            DbSet.Remove(Value);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
           var results = await DbSet.OrderByDescending(x => x).ToListAsync();
            if (results.Count is 0)
                throw new CollectionIsEmptyException();

            return results;
        }

        public async Task<T> GetById(int id)
        {
             var result = await DbSet.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (result is null)
                throw new ObjectIsNullException();            
            return result;
        }


        public void Update(T Value)
        {
            DbSet.Update(Value);
        }
    }
}
