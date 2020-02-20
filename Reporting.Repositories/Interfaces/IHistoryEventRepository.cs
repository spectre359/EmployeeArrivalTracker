using Reporting.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Reporting.Repositories.Interfaces
{
    public interface IHistoryEventRepository
    {
        Task AddEvent();
        Task<bool> EventExists();
    }
}
