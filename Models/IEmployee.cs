using Employees_API.DTOs.Employees;
using Employees_API.Interfaces;

namespace Employees_API.Models
{
    public interface IEmployee : IIdentification, IModel
    {
        string? Address { get; set; }
        //Department Department { get; set; }
        //int DepartmentId { get; set; }
        string Email { get; set; }
        int Id { get; set; }
        bool IsAvailable { get; set; }
        string Name { get; set; }

       
    }
}