using Reporting.Repositories.Interfaces;
using Reporting.Services.Interfaces;
using System.Threading.Tasks;

namespace Reporting.Services
{
    public class HistoryEventsService : IHistoryEventsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public HistoryEventsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddEvent()
        {
            await _unitOfWork.HistoryEvents.AddEvent();
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> EventExists()
        {
            return await _unitOfWork.HistoryEvents.EventExists();         
        }
    }
}
