using Reporting.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reporting.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<bool> AddMany(List<Employee> employees);
        Task<List<Employee>> GetAll();
    }
}
