using Reporting.Contracts.Misc;
using Reporting.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reporting.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task AddMany(List<Employee> employees);
        Task<IQueryable<Employee>> GetAll(SearchFilter filter);
    }
}
