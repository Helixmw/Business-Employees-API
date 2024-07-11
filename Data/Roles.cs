using Employees_API.Models;

namespace Employees_API.Data
{
    public class Roles : BaseContext<Role>
    {
        public Roles(ApplicationDBContext applicationDBContext) : base(applicationDBContext.Roles, applicationDBContext)
        {
            
        }
    }
}
