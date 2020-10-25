using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseRegistration.Model;
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
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Student student = _studentRepository.Get(id);
            if (student == null)
            {
                return BadRequest();
            }
            return Ok(student);
        }
    }
}
