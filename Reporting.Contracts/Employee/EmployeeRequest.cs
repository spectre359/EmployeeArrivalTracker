using System;
using System.Collections.Generic;
using System.Text;

namespace Reporting.Contracts.Employee
{
    public class EmployeeRequest
    {
        public int Id { get; set; }
        public int? ManagerId { get; set; }
        public int Age { get; set; }
        public string[] Teams { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string SurName { get; set; }
        public string Name { get; set; }
       
    }
}
