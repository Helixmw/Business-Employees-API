using Employees_API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Employees_API.Exceptions;
using Employees_API.Data;

namespace Employees_API.Utilities
{
    public class BaseContext<T> : IBaseContext<T> where T : class, IIdentification
    {

        private readonly DbSet<T> DbSet = null!;

        private readonly ApplicationDBContext _dbContext = null!;
        public BaseContext(DbSet<T> DbSet, ApplicationDBContext dBContext)
        {
            this.DbSet = DbSet;
            _dbContext = dBContext;
        }
        public async Task AddAsync(T Value, Action? Save = null)
        {
            await DbSet.AddAsync(Value);
            if (Save == null)
                await _dbContext.SaveChangesAsync();

            else
                Save();

            

        }

        public async void Delete(T Value)
        {
            DbSet.Remove(Value);
            await _dbContext.SaveChangesAsync();
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
