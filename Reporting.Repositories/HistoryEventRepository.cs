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
    public class HistoryEventRepository : IHistoryEventRepository
    {
        private readonly ReportingToolContext _context;

        public HistoryEventRepository(ReportingToolContext context)
        {
            _context = context;
        }

        public async Task AddEvent()
        {
            var ev = new HistoryAPICallEvent()
            {
                LastCallAt = DateTime.UtcNow
            };
            await _context.AddAsync(ev);
        }

        public async Task<bool> EventExists()
        {
            return _context.HistoryEvents.Any(e=>e.LastCallAt.Date == DateTime.Today.Date);
        }
    }
}
