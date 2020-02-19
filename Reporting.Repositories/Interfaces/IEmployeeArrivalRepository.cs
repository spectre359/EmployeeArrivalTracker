using Reporting.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting.Repositories.Interfaces
{
    public interface IEmployeeArrivalRepository
    {
        Task AddMany(List<EmployeeArrival> arrivals);
        Task<IQueryable<EmployeeArrival>> GetAll();
    }
}
