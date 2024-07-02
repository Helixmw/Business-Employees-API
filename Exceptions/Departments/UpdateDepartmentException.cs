using Employees_API.Interfaces;

namespace Employees_API.Exceptions.Departments
{
    public class UpdateDepartmentException : Exception
    {
        public UpdateDepartmentException(string massage):base(massage)
        {
            
        }
    }
}
