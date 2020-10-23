using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseRegistration.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CourseRegistration.Repositories
{
    public class DepartmentRepository
    {
        private readonly IConfiguration _config;
        public DepartmentRepository(IConfiguration configuration)
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
        public List<Department> GetAllDepartments()
        {
            using (var conn = connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, [DepartmentName] FROM Department;";
                    var reader = cmd.ExecuteReader();
                    var departments = new List<Department>();
                    while (reader.Read())
                    {
                        var department = new Department()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            DepartmentName = reader.GetString(reader.GetOrdinal("DepartmentName")),
                        };

                        departments.Add(department);
                    }

                    reader.Close();

                    return departments;
                }
            }
         
        }
        public Department Get(int id)
        {
            using (var conn = connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, DepartmentName from Department where Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();
                    Department department = null;
                    while (reader.Read())
                    {
                        department = new Department
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            DepartmentName = reader.GetString(reader.GetOrdinal("DepartmentName"))
                        };
                    }
                    reader.Close();
                    return department;
                }
            }
        }
    }
}
