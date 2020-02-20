using Reporting.Contracts.Employee;
using Reporting.Contracts.Misc;
using Reporting.Contracts.ViewModels;
using Reporting.Data.Entities;
using Reporting.Repositories.Interfaces;
using Reporting.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporting.Services
{
    public class EmployeeArrivalsService : IEmployeeArrivalsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeArrivalsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> AddMany(List<EmployeeArrivalRequest> arrivals)
        {
            try
            {
                var entities = new List<EmployeeArrival>();
                arrivals.ForEach(a => entities.Add(AutoMapper.Mapper.Map<EmployeeArrival>(a)));
                await _unitOfWork.Arrivals.AddMany(entities);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {

            }
            return true;
        }

        public async Task<PaginatedList<EmployeeArrivalViewModel>> GetViewArrivals(int pageNumber, int pageSize, SearchFilter searchFilter)
        {
            var arrivals = await _unitOfWork.Arrivals.GetAll(searchFilter);
            if (arrivals.Count() == 0)
            {
                return await ConstructViewModels(arrivals, new List<Employee>().AsQueryable(), pageNumber, pageSize, 0);
            }
            if (searchFilter == null)
            {
                searchFilter = new SearchFilter();
            }
            //all employees that arrived this day
            searchFilter.EmployeeIds = arrivals.Select(a => a.EmployeeId).ToList();
            //filter employees by name/position etc
            var employees = await _unitOfWork.Employees.GetAll(searchFilter);
            //filtered employee ids 
            searchFilter.EmployeeIds = employees.Select(e => e.Id).ToList();
            //filter arrivals by filtered employees
            arrivals = arrivals.Where(a => searchFilter.EmployeeIds.Contains(a.EmployeeId));
            int totalCount = employees.Count();

            switch (searchFilter.SelectedSortOption)
            {
                case "Name ^":
                    employees = employees.OrderByDescending(e => e.Name);
                    break;
                case "Job ^":
                    employees = employees.OrderByDescending(e => e.Role);
                    break;
                case "Job":
                    employees = employees.OrderBy(e => e.Role);
                    break;
                default:
                    employees = employees.OrderBy(e => e.Name);
                    break;
            }

            //take only 12 or whatever is the pageSize
            employees = employees.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return await ConstructViewModels(arrivals, employees, pageNumber, pageSize, totalCount);
        }

        private async Task<PaginatedList<EmployeeArrivalViewModel>> ConstructViewModels(IQueryable<EmployeeArrival> arrivals, IQueryable<Employee> employees, int pageNumber, int pageSize, int totalCount)
        {

            var qry = new List<EmployeeArrivalViewModel>();
            foreach (var employee in employees)
            {
                var arrival = arrivals.FirstOrDefault(e => e.EmployeeId == employee.Id);
                qry.Add(new EmployeeArrivalViewModel
                {
                    JobPosition = employee.Role,
                    Name = $"{employee.Name} {employee.SurName}",
                    When = arrival.When
                });
            }


            return await PaginatedList<EmployeeArrivalViewModel>.CreateAsync(qry.AsQueryable(), pageNumber, pageSize, totalCount);
        }
    }
}
