using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseRegistration.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CourseRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        public readonly StudentRepository _studentRepository;
        public StudentController(IConfiguration config)
        {
            _studentRepository = new StudentRepository(config);
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_studentRepository.GetAll());
        }
    }
}
