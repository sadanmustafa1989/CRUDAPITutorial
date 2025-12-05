using CRUDAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeReposiitory _repo;


        public EmployeeController(EmployeeReposiitory repo)
        {
            _repo = repo;
        }


        [HttpGet]
        public IActionResult GetAll() => Ok(_repo.GetEmployees());

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var emp = _repo.GetEmployeeByID(id);
            return emp == null ? NotFound() : Ok(emp);
        }

        [HttpPost]
        public IActionResult Insert(Employee emp)
        {
            _repo.InsertEmployee(emp);
            return Ok("Employee Added");
        }

        [HttpPut]
        public IActionResult Update(Employee emp)
        {
            _repo.UpdateEmployee(emp);
            return Ok("Employee Updated");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repo.DeleteEmployee(id);
            return Ok("Employee Deleted");
        }

    }
}
