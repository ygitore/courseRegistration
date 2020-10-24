using CourseRegistration.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseRegistration.Repositories
{
    public class CourseRepository
    {
        private readonly IConfiguration _config;
        public CourseRepository(IConfiguration configuration)
        {
            _config = configuration;
        }
        public SqlConnection connection
        {
            get 
            { 
                return new SqlConnection(_config.GetConnectionString("DefaultConnection")); 
            }
        }
        public List<Course> GetAll()
        {
            using(var conn = connection)
            {
                conn.Open();
                using(var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Course";
                    var reader = cmd.ExecuteReader();
                    var courses = new List<Course>(); 
                    while (reader.Read())
                    {
                        var course = new Course
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            CourseName = reader.GetString(reader.GetOrdinal("CourseName")),
                            CourseCode = reader.GetString(reader.GetOrdinal("CourseCode"))
                        };
                        courses.Add(course);
                    }
                    reader.Close();
                    return courses;
                }
            }
        }

        public Course Get(int id)
        {
            using(var conn = connection)
            {
                conn.Open();
                using(var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Course where Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();
                    Course course = null;
                    while (reader.Read())
                    {
                        course = new Course
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            CourseName = reader.GetString(reader.GetOrdinal("CourseName")),
                            CourseCode = reader.GetString(reader.GetOrdinal("CourseCode"))
                        };
                    }
                    reader.Close();
                    return course;
                }
            }
        }
        public void Add(Course course)
        {
            using(var conn = connection)
            {
                conn.Open();
                using(var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "Insert Into Course (CourseName, CourseCode) OUTPUT INSERTED.ID Values(@cName, @cCode)";
                    cmd.Parameters.AddWithValue("@cName", course.CourseName);
                    cmd.Parameters.AddWithValue("@cCode", course.CourseCode);
                    //executes sql query and returns very first row of first column then converts result into integer and assigned to new department Id
                    course.Id = (int)cmd.ExecuteScalar();
                };
            };
        }
    }
}
