using Moq;
using Reporting.Contracts.Employee;
using Reporting.Contracts.Misc;
using Reporting.Data.Entities;
using Reporting.Repositories.Interfaces;
using Reporting.Services.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Reporting.Services.Tests
{
    public class EmployeeArrivalsServiceTests
    {
        private Mock<IEmployeeArrivalRepository> _repositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<ILogger> _logger;
        public EmployeeArrivalsServiceTests()
        {
            _repositoryMock = new Mock<IEmployeeArrivalRepository>();
            _logger = new Mock<ILogger>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(uow => uow.Arrivals).Returns(_repositoryMock.Object);
        }


        [Fact]
        public async Task DoesAdd()
        {
            _repositoryMock.Setup(x => x.AddMany(It.IsAny<List<EmployeeArrival>>()));

            var arrivals = new List<EmployeeArrivalRequest>();
            var service = new EmployeeArrivalsService(_unitOfWorkMock.Object, _logger.Object);
            await service.AddMany(arrivals);

            _repositoryMock.Verify(x => x.AddMany(It.IsAny<List<EmployeeArrival>>()), Times.Once);
        }

        [Fact]
        public async Task DoesGet()
        {
            _repositoryMock.Setup(x => x.GetAll(It.IsAny<SearchFilter>()));
            var filter = new SearchFilter();
            int pageNumber = 1;
            int pageSize = 12;
            var service = new EmployeeArrivalsService(_unitOfWorkMock.Object, _logger.Object);
            var result = await service.GetViewArrivals(pageNumber, pageSize, filter);

            _repositoryMock.Verify(x => x.GetAll(It.IsAny<SearchFilter>()), Times.Once);
            
        }
    }
}
