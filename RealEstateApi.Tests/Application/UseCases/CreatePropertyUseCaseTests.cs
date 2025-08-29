using Moq;
using RealEstateApi.Application.Interfaces;
using RealEstateApi.Domain.Interfaces;

namespace YourApp.Tests.UseCases
{
    [TestFixture]
    public class CreatePropertyUseCaseTests
    {
        private Mock<IPropertyRepository> _repositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private CreatePropertyUseCaseTests _useCase;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IPropertyRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _useCase = new CreatePropertyUseCaseTests(_repositoryMock.Object, _unitOfWorkMock.Object);
        }

        [Test]
        public async Task ExecuteAsync_ShouldRollback_WhenRepositoryThrowsException()
        {
            // Arrange
            var dto = new CreatePropertyDto
            {
                Name = "Casa en Bogotá",
                Address = "Calle 123"
            };

            _unitOfWorkMock.Setup(u => u.BeginTransactionAsync()).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(u => u.RollbackAsync()).Returns(Task.CompletedTask);

            _repositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Property>()))
                .ThrowsAsync(new Exception("Simulated failure"));

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await _useCase.ExecuteAsync(dto));

            // Verify transaction flow
            _unitOfWorkMock.Verify(u => u.BeginTransactionAsync(), Times.Once);
            _unitOfWorkMock.Verify(u => u.RollbackAsync(), Times.Once);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Never);
        }
    }
}

