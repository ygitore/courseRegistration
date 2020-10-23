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
            return Ok(_departmentRepository.Get(id));
        }       
    }
}
