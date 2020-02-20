using Reporting.Contracts.Employee;
using Reporting.Contracts.Misc;
using Reporting.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting.Services.Interfaces
{
    public interface IEmployeeArrivalsService
    {
        Task<PaginatedList<EmployeeArrivalViewModel>> GetViewArrivals(int pageNumber, int pageSize, SearchFilter searchFilter);
        Task<bool> AddMany(List<EmployeeArrivalRequest> arrivals);
    }
}
