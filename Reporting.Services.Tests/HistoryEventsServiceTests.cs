using Moq;
using Reporting.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reporting.Services.Tests
{
    public class HistoryEventsServiceTests
    {
        private Mock<IHistoryEventRepository> _repositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;

        public HistoryEventsServiceTests()
        {
            _repositoryMock = new Mock<IHistoryEventRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(uow => uow.HistoryEvents).Returns(_repositoryMock.Object);
        }

        [Fact]
        public async Task DoesAdd()
        {
            _repositoryMock.Setup(x => x.AddEvent());
                       
            var service = new HistoryEventsService(_unitOfWorkMock.Object);
            await service.AddEvent();

            _repositoryMock.Verify(x => x.AddEvent(), Times.Once);
        }

        [Fact]
        public async Task DoesExist()
        {
            _repositoryMock.Setup(x => x.EventExists());

            var service = new HistoryEventsService(_unitOfWorkMock.Object);
            var result = await service.EventExists();

            _repositoryMock.Verify(x => x.EventExists(), Times.Once);
            
        }

    }
}
