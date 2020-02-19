using Reporting.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Reporting.Repositories.Interfaces
{
    public interface IEmployeeArrivalRepository
    {
        Task<int> AddMany(List<EmployeeArrival> arrivals);
        Task<List<EmployeeArrival>> GetAll();
    }
}
