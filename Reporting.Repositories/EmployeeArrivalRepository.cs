using Reporting.Data.Context;
using Reporting.Data.Entities;
using Reporting.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Reporting.Repositories
{
    public class EmployeeArrivalRepository : IEmployeeArrivalRepository
    {
        private readonly ReportingToolContext _context;

        public EmployeeArrivalRepository(ReportingToolContext context)
        {
            _context = context;
        }

        public Task<int> AddMany(List<EmployeeArrival> arrivals)
        {
            throw new NotImplementedException();
        }

        public Task<List<EmployeeArrival>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
