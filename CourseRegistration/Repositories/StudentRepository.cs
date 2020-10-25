using CourseRegistration.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseRegistration.Repositories
{
    public class StudentRepository
    {
        public readonly IConfiguration _config;
        public StudentRepository(IConfiguration config)
        {
            _config = config;
        }
        public SqlConnection connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }
        public List<Student> GetAll()
        {
            using(var conn = connection)
            {
                conn.Open();
                using(var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, firstName, lastName, age, genderId, email,departmentId, instructorId from student";
                    SqlDataReader reader = cmd.ExecuteReader();
                    var students = new List<Student>();
                    while (reader.Read())
                    {
                        Student student = new Student
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("firstName")),
                            LastName = reader.GetString(reader.GetOrdinal("lastName")),
                            Email = reader.GetString(reader.GetOrdinal("email")),
                            Age = reader.GetInt32(reader.GetOrdinal("age")),
                            GenderId = reader.GetInt32(reader.GetOrdinal("genderId")),
                            DepartmentId = reader.GetInt32(reader.GetOrdinal("genderId")),
                            InstructorId = reader.GetInt32(reader.GetOrdinal("instructorId"))
                        };
                        students.Add(student);
                    }
                    reader.Close();
                    return students;
                };
            }
        }
    }
}
