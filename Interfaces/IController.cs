using Microsoft.AspNetCore.Mvc;

namespace Employees_API.Interfaces
{
    public interface IController<T, U> where T : class where U : class
    {
        [HttpGet]
        Task<IActionResult> Get();

        [HttpPost]
        Task<IActionResult> Post(T Value);

        [HttpGet("{id}")]
        Task<IActionResult> GetById(int id);

        [HttpDelete("{id}")]
        Task<IActionResult> DeleteById(int id);

        [HttpPut]
        Task<IActionResult> Update(U Value);


    }
}
