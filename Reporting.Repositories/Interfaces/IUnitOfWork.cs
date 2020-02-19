using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Reporting.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IEmployeeRepository Employees { get; }
        IEmployeeArrivalRepository Arrivals { get; }
        Task SaveAsync();
    }
}
