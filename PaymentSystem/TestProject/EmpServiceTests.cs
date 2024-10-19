using Moq;
using PaymentSystem.Application.Emp;
using PaymentSystem.Models;

namespace TestProject
{
    public class EmpServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void AddEmpTest()
        {
            // Arrange
            var emp = new EmpCore
            {
                Id = "1",
                Name = "John",
                Address = "New York"
            };
            var empRepository = new Mock<IEmpRepository>();
            empRepository.Setup(x => x.Add(emp));
            var empService = new EmpService(empRepository.Object);

            // Act
            empService.AddEmp(emp);

            // Assert
            empRepository.Verify(x => x.Add(emp), Times.Once);
        }
    }
}