using Microsoft.EntityFrameworkCore;
using Reporting.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reporting.Data.Context
{
    public class ReportingToolContext : DbContext
    {
        public ReportingToolContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeArrival> EmployeeArrivals { get; set; }
    }
}
