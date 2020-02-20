using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reporting.Contracts.ViewModels
{
    public class EmployeeArrivalViewModel
    {
        public string Name { get; set; }
        public DateTime When { get; set; }
        public string JobPosition { get; set; }
    }
}
