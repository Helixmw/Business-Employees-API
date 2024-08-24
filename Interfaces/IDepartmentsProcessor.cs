using Employees_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Employees_API.Interfaces
{
    public interface IDepartmentsProcessor : IBaseContext<Department>
    {
        void RemoveDeptEmployees(Department dept);

        void SaveDeptChanges();


    }
}
