using Employees_API.Models;

namespace Employees_API.Data
{
    public class Departments : BaseContext<Department>
    {
        public Departments(ApplicationDBContext applicationDBContext) : base(applicationDBContext.Departments)
        {
            
        }
    }
}
