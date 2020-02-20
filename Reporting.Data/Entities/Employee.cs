using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Reporting.Data.Entities
{
    public class Employee
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int Id { get; set; }        
        public int Age { get; set; }             
        public string Role { get; set; }
        public string Email { get; set; }
        public string SurName { get; set; }
        public string Name { get; set; }
       
    }
}
