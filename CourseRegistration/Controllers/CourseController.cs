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
    public class CourseController : ControllerBase
    {
        private readonly CourseRepository _courseRepository;
        public CourseController(IConfiguration config)
        {
            _courseRepository = new CourseRepository(config);
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_courseRepository.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Course course = _courseRepository.Get(id);
            if(course == null)
            {
                return BadRequest();
            }
            return Ok(course);
        }
        public IActionResult Post(Course course)
        {
            _courseRepository.Add(course);
            return Ok(course);
        }
    }
}
