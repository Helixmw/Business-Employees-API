using Employees_API.Interfaces;
using Employees_API.Models;

namespace Employees_API.Data
{
    public class Employees : BaseContext<Employee>
    {
        public Employees(ApplicationDBContext applicationDBContext) : base(applicationDBContext.Employees)
        {

        }      
    }
}
