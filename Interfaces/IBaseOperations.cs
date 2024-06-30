namespace Employees_API.Interfaces
{
    public interface IBaseOperations<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
    }
}
