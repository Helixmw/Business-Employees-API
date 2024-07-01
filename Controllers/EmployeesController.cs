using Employees_API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employees_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
         public Employees Employees { get; set; }

        public EmployeesController(ApplicationDBContext applicationDBContext)
        {
            Employees = new Employees(applicationDBContext); 
        }

        

    }
}
