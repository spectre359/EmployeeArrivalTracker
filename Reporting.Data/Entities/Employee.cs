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
        [Key]
        public int Id { get; set; }
        public int? ManagerId { get; set; }
        public int Age { get; set; }
        [NotMapped]
        public string[] Teams
        {
            get
            {
                return TeamsData.Split(';');
            }
            set
            {
                TeamsData = String.Join(";", value.Select(p => p.ToString()).ToArray());
            }
        }
        public string TeamsData { get; set; }       
        public string Role { get; set; }
        public string Email { get; set; }
        public string SurName { get; set; }
        public string Name { get; set; }

        [ForeignKey("ManagerId")]
        public virtual Employee Manager { get; set; }
    }
}
