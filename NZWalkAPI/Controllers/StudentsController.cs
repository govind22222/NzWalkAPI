using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        // https://localhost:portnum/api/students/GetAllStudents
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] stunames = new string[] { "Raghav", "Raghvendra", "Happy", "Sandeep" };
            return Ok(stunames);
        }

    }
}
