using System;
using System.Collections.Generic;
using System.Text;

namespace Reporting.Contracts.Employee
{
    public class EmployeeArrivalResponse
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public System.DateTime When { get; set; }
    }
}
