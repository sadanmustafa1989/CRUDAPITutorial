using CRUDAPI.Models;
using System.Data;
using Microsoft.Data.SqlClient;

namespace CRUDAPI
{
    public class EmployeeReposiitory
    {

        private readonly string _connecitonString;

        public EmployeeReposiitory(IConfiguration configuration)
        {
            _connecitonString = configuration.GetConnectionString("Default");
        }


        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();

            using SqlConnection conn = new(_connecitonString);
            using SqlCommand cmd = new("spGetAllEmployees", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            conn.Open();

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                employees.Add(new Employee
                {
                    ID = (int)reader["ID"],
                    EmployeeName = (string)reader["EmployeeName"],
                    EmployeeEmail = (string)reader["EmployeeEmail"],
                    EmployeeSalary = (decimal)reader["EmployeeSalary"]
                });


            }

            return employees;
        }

        public void InsertEmployee(Employee emp)
        {
            using SqlConnection conn = new(_connecitonString);
            using SqlCommand cmd = new("spInsertEmployee", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
            cmd.Parameters.AddWithValue("@EmployeeEmail", emp.EmployeeEmail);
            cmd.Parameters.AddWithValue("@EmployeeSalary", emp.EmployeeSalary);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void UpdateEmployee(Employee emp)
        {
            using SqlConnection conn = new(_connecitonString);
            using SqlCommand cmd = new("spUpdateEmployee", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ID", emp.ID);
            cmd.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
            cmd.Parameters.AddWithValue("@EmployeeEmail", emp.EmployeeEmail);
            cmd.Parameters.AddWithValue("@EmployeeSalary", emp.EmployeeSalary);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void DeleteEmployee(int ID)
        {
            using SqlConnection conn = new(_connecitonString);
            using SqlCommand cmd = new("spDeleteEmployee", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ID", ID);

            conn.Open();
            cmd.ExecuteNonQuery();
        }


        public Employee GetEmployeeByID(int ID)
        {
            Employee employee = null;

            using SqlConnection conn = new(_connecitonString);
            using SqlCommand cmd = new("spGetEmployeeByID", conn);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@ID", ID);


            conn.Open();

            using SqlDataReader reader = cmd.ExecuteReader();

            if(reader.Read())
            {
                employee = new Employee
                {
                    ID = (int)reader["ID"],
                    EmployeeName = (string)reader["EmployeeName"],
                    EmployeeEmail = (string)reader["EmployeeEmail"],
                    EmployeeSalary = (decimal)reader["EmployeeSalary"]
                };
            }

            return employee;
        }

    }
}
