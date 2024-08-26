using Employees_API.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Employees_API.Models
{
    public class Role : IIdentification, IModel
    {

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }


    }
}
