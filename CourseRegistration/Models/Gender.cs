using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseRegistration.Model
{
    public class Gender
    {
        public int Id { get; set; }
        [Required]
        public string gender { get; set; }
    }
}
