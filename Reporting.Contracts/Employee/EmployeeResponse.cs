using System;
using System.Collections.Generic;
using System.Text;

namespace Reporting.Contracts.Employee
{
    public class EmployeeResponse
    {
        public int Id { get; set; } 
        public string Role { get; set; }
        public string SurName { get; set; }
        public string Name { get; set; }
    }
}
