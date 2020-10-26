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
    public class InstructorController : ControllerBase
    {
        public readonly InstructorRepository _instructorRepository;
        public InstructorController(IConfiguration config)
        {
            _instructorRepository = new InstructorRepository(config);
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_instructorRepository.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Instructor instructor = _instructorRepository.Get(id);
            if (instructor == null)
            {
                return BadRequest();
            }
            return Ok(instructor);
        }
        [HttpPost]
        public IActionResult Post(Instructor instructor)
        {
            if (instructor == null)
            {
                return BadRequest();
            }
            _instructorRepository.Add(instructor);
            return Ok(instructor);
        }
    }
}
