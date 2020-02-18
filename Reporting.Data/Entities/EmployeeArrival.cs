using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Reporting.Data.Entities
{
    public class EmployeeArrival
    {
        [Key]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public System.DateTime When { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}
