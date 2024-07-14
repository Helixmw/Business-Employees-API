using Employees_API.Data;
using Employees_API.Interfaces;
using Employees_API.Models;

namespace Employees_API.Utilities
{
    public class Roles : BaseContext<Role>, IRoles
    {
        public Roles(ApplicationDBContext applicationDBContext) : base(applicationDBContext.EmployeeRoles, applicationDBContext)
        {

        }
    }
}
