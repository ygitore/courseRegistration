using CourseRegistration.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseRegistration.Repositories
{
    public class InstructorRepository
    {
        public readonly IConfiguration _config;
        public InstructorRepository(IConfiguration config)
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
        public List<Instructor> GetAll()
        {
            using (var conn = connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, firstName, lastName, age, genderId, email,departmentId from Instructor";
                    SqlDataReader reader = cmd.ExecuteReader();
                    var instructors = new List<Instructor>();
                    while (reader.Read())
                    {
                        Instructor instructor = new Instructor
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("firstName")),
                            LastName = reader.GetString(reader.GetOrdinal("lastName")),
                            Email = reader.GetString(reader.GetOrdinal("email")),
                            Age = reader.GetInt32(reader.GetOrdinal("age")),
                            GenderId = reader.GetInt32(reader.GetOrdinal("genderId")),
                            DepartmentId = reader.GetInt32(reader.GetOrdinal("genderId")),
                        };
                        instructors.Add(instructor);
                    }
                    reader.Close();
                    return instructors;
                };
            }
        }
        public Instructor Get(int id)
        {
            using (var conn = connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, firstName, lastName, age, genderId, email,departmentId from Instructor where Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    Instructor instructor = null;
                    while (reader.Read())
                    {
                        instructor = new Instructor
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("firstName")),
                            LastName = reader.GetString(reader.GetOrdinal("lastName")),
                            Email = reader.GetString(reader.GetOrdinal("email")),
                            Age = reader.GetInt32(reader.GetOrdinal("age")),
                            GenderId = reader.GetInt32(reader.GetOrdinal("genderId")),
                            DepartmentId = reader.GetInt32(reader.GetOrdinal("genderId")),                        };
                    }
                    reader.Close();
                    return instructor;
                };
            };
        }
        public void Add(Instructor instructor)
        {
            using (var conn = connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "Insert Into Instructor (firstName, lastName, age, genderId, email,departmentId) OUTPUT INSERTED.ID Values(@fName,@lName, @age,@genderid, @email, @departmentId) ";
                    cmd.Parameters.AddWithValue("@fName", instructor.FirstName);
                    cmd.Parameters.AddWithValue("@lName", instructor.LastName);
                    cmd.Parameters.AddWithValue("@age", instructor.Age);
                    cmd.Parameters.AddWithValue("@email", instructor.Email);
                    if (instructor.GenderId.ToString() == null)
                    {
                        cmd.Parameters.AddWithValue("@genderId", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@genderId", instructor.GenderId);
                    }

                    if (instructor.DepartmentId.ToString() == null)
                    {
                        cmd.Parameters.AddWithValue("@departmentId", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@departmentId", instructor.DepartmentId);
                    }                    

                    instructor.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}
