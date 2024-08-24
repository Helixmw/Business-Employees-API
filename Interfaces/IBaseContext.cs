namespace Employees_API.Interfaces
{
    public interface IBaseContext<T> where T : class
    {
        
        Task AddAsync(T Value, Action? Save = null);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetById(int id);
        void Update(T Value);

        void Delete(T Value);
    }
}
