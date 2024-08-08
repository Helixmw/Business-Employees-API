using Employees_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Employees_API.Interfaces
{
    public interface IDepartments : IBaseContext<Department>
    {
        void RemoveDeptEmployees(int departmentId);

        void SaveDeptChanges();


    }
}
