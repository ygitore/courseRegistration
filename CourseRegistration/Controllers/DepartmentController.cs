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
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentRepository _departmentRepository;
        public DepartmentController(IConfiguration config)
        {
            _departmentRepository = new DepartmentRepository(config);
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_departmentRepository.GetAllDepartments());
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var department = _departmentRepository.Get(id);
            if(department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }
        [HttpPost]
        public IActionResult Post(Department department)
        {
            _departmentRepository.Add(department);
            return CreatedAtAction("Get", new { id = department.Id }, department);
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, Department department)
        {
            if(id != department.Id)
            {
                return BadRequest();
            }
            _departmentRepository.Update(department);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _departmentRepository.Delete(id);
            return NoContent();
        }
    }
}
