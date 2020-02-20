using Moq;
using Reporting.Contracts.Misc;
using Reporting.Data.Entities;
using Reporting.Repositories;
using Reporting.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reporting.Tests
{
    public class EmployeeRepositoryTests
    {
        private Mock<IEmployeeRepository> _repositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;

        public EmployeeRepositoryTests()
        {
            _repositoryMock = new Mock<IEmployeeRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(uow => uow.Employees).Returns(_repositoryMock.Object);
        }


        [Fact]
        public async Task DoesAdd()
        {
            _repositoryMock.Setup(x => x.AddMany(It.IsAny<List<Employee>>()));

            var employees = new List<Employee>();
            var repository = _unitOfWorkMock.Object.Employees;
            await repository.AddMany(employees);

            _repositoryMock.Verify(x => x.AddMany(It.IsAny<List<Employee>>()), Times.Once);
        }

        [Fact]
        public async Task DoesGet()
        {
            _repositoryMock.Setup(x => x.GetAll(It.IsAny<SearchFilter>()));

            var filter = new SearchFilter();
            var repository = _unitOfWorkMock.Object.Employees;
            await repository.GetAll(filter);

            _repositoryMock.Verify(x => x.GetAll(It.IsAny<SearchFilter>()), Times.Once);
        }
    }
}
