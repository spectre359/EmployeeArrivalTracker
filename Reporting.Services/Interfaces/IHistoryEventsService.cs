using System.Threading.Tasks;

namespace Reporting.Services.Interfaces
{
    public interface IHistoryEventsService
    {
        Task AddEvent();
        Task<bool> EventExists();
    }
}
