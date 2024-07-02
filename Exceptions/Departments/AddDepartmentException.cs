using Employees_API.Interfaces;

namespace Employees_API.Exceptions.Departments
{
    public class AddDepartmentException : Exception
    {
        public AddDepartmentException(string message):base(message)
        {
            
        }
    }
}
