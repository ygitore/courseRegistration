using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseRegistration.Model
{
    public class Instructor
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength =1)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string LastName { get; set; }
        [Required]
        public int Age { get; set; }
        public int GenderId { get; set; }
        public Gender Gender { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 7)]
        public string Email { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
