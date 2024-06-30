using Employees_API.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            return await DbSet.OrderByDescending(x => x).ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
             var results = await DbSet.Where(x => x.Id == id).FirstOrDefaultAsync();
            return results;
        }


        public void Update(T Value)
        {
            DbSet.Update(Value);
        }
    }
}
