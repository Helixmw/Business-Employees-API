using Employees_API.Data;
using Employees_API.Interfaces;
using Employees_API.Models;

namespace Employees_API.Utilities
{
    public class RolesProcessor : BaseContext<Role>, IRoles
    {
        public RolesProcessor(ApplicationDBContext applicationDBContext) : base(applicationDBContext.EmployeeRoles, applicationDBContext)
        {

        }
    }
}
