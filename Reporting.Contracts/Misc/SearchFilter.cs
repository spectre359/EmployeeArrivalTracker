using System;
using System.Collections.Generic;
using System.Text;

namespace Reporting.Contracts.Misc
{
    public class SearchFilter
    {
        public SearchFilter()
        {
            EmployeeIds = new List<int>();
            When = DateTime.Today;
            SelectedSortOption = "Name";
            SortOptions = new List<string>()
            {
                "Name",
                "Name ^",
                "Job",
                "Job ^"
            };
        }
        public string Name { get; set; }
        public DateTime When { get; set; }
        public string JobPosition { get; set; }
        public string SelectedSortOption { get; set; }
        public List<string> SortOptions { get; set; }
        public List<int> EmployeeIds { get; set; }
    }
}
