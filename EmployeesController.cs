using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using EmployeeManagerWebAPI.Models;

namespace EmployeeManagerWebAPI.Controllers
{
    public class EmployeesController : ApiController
    {
        private static List<Employee> employees = new List<Employee>()
        {
            new Employee { Id = 1, Name = "John", Age = 35 },
            new Employee { Id = 2, Name = "May", Age = 24 },
            new Employee { Id = 3, Name = "Peter", Age = 21 },
            new Employee { Id = 4, Name = "Anthony", Age = 40 },
            new Employee { Id = 5, Name = "April", Age = 32 }
        };

        private static List<Department> departments = new List<Department>()
        {
            new Department { EmpId = 1, Designation = "Team Lead" },
            new Department { EmpId = 2, Designation = "Developer" },
            new Department { EmpId = 3, Designation = "Developer" },
            new Department { EmpId = 4, Designation = "Senior Developer" },
            new Department { EmpId = 5, Designation = "Senior Developer" }
        };

        public IHttpActionResult GetEmployees()
        {
            var employeeProfiles = employees.Join(
                departments,
                emp => emp.Id,
                dept => dept.EmpId,
                (emp, dept) => new Employee
                {
                    Name = emp.Name,
                    Age = emp.Age,
                    Designation = dept.Designation
                }).ToList();

            return Ok(employeeProfiles);
        }

        [HttpGet]
        [Route("api/employees/youngest")]
        public IHttpActionResult GetYoungestEmployee()
        {
            var youngestEmployee = employees.OrderBy(e => e.Age).FirstOrDefault();
            if (youngestEmployee == null)
                return NotFound();

            var youngestEmployeeProfile = new Employee
            {
                Name = youngestEmployee.Name,
                Age = youngestEmployee.Age,
                Designation = departments.FirstOrDefault(d => d.EmpId == youngestEmployee.Id)?.Designation
            };

            return Ok(youngestEmployeeProfile);
        }

        [HttpGet]
        [Route("api/employees/eldest")]
        public IHttpActionResult GetEldestEmployee()
        {
            var eldestEmployee = employees.OrderByDescending(e => e.Age).FirstOrDefault();
            if (eldestEmployee == null)
                return NotFound();

            var eldestEmployeeProfile = new Employee
            {
                Name = eldestEmployee.Name,
                Age = eldestEmployee.Age,
                Designation = departments.FirstOrDefault(d => d.EmpId == eldestEmployee.Id)?.Designation
            };

            return Ok(eldestEmployeeProfile);
        }
    }
}


       
