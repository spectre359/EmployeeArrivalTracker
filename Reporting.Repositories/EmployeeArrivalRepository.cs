using Reporting.Contracts.Misc;
using Reporting.Data.Context;
using Reporting.Data.Entities;
using Reporting.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task AddMany(List<EmployeeArrival> arrivals)
        {
            await _context.AddRangeAsync(arrivals);
        }

        public async Task<IQueryable<EmployeeArrival>> GetAll(SearchFilter filter = null)
        {
            var qry = _context.EmployeeArrivals.Where(a => a.When.Date == (filter != null ? filter.When.Date : DateTime.Today.Date));
            if (filter != null)
            {
                if (filter.EmployeeIds.Count > 0)
                {
                    qry = qry.Where(c => filter.EmployeeIds.Contains(c.EmployeeId));
                }               
            }
            return qry;
        }
    }
}
