using Reporting.Data.Context;
using Reporting.Repositories.Interfaces;
using System.Threading.Tasks;

namespace Reporting.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ReportingToolContext _context;
        private IEmployeeRepository _employeeRepository;
        private IEmployeeArrivalRepository _employeeArrivalRepository;
        private IHistoryEventRepository _historyEventRepository;
        public UnitOfWork(ReportingToolContext context)
        {
            _context = context;
        }

        public IEmployeeRepository Employees
        {
            get
            {
                if (_employeeRepository == null)
                {
                    _employeeRepository = new EmployeeRepository(_context);
                }
                return _employeeRepository;
            }
        }

        public IEmployeeArrivalRepository Arrivals
        {
            get
            {
                if (_employeeArrivalRepository == null)
                {
                    _employeeArrivalRepository = new EmployeeArrivalRepository(_context);
                }
                return _employeeArrivalRepository;
            }
        }

        public IHistoryEventRepository HistoryEvents
        {
            get
            {
                if (_historyEventRepository == null)
                {
                    _historyEventRepository = new HistoryEventRepository(_context);
                }
                return _historyEventRepository;
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
