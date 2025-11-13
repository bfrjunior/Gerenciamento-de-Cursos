using Xunit;
using Moq;
using Gerenciamento_cursos.Services.Aluno;
using Gerenciamento_cursos.Repositories;
using Gerenciamento_cursos.Model;
using Gerenciamento_cursos.Validators;

namespace Gerenciamento_cursos.Tests
{
    public class AlunoServiceTests
    {
        private readonly Mock<IRepository<AlunoModel>> _mockRepository;
        private readonly Mock<IAlunoValidator> _mockValidator;
        private readonly AlunoService _service;

        public AlunoServiceTests()
        {
            _mockRepository = new Mock<IRepository<AlunoModel>>();
            _mockValidator = new Mock<IAlunoValidator>();
            _service = new AlunoService(_mockRepository.Object, _mockValidator.Object);
        }

        [Fact]
        public async Task AddAsync_WithValidAluno_ReturnsSuccess()
        {
            // Arrange
            var aluno = new AlunoModel 
            { 
                Nome = "João Silva", 
                Email = "joao@example.com", 
                DataNascimento = new DateTime(2005, 1, 1) 
            };
            
            _mockValidator.Setup(v => v.ValidateAsync(aluno, false))
                .ReturnsAsync(ValidationResultModel.SuccessResult());
            
            _mockRepository.Setup(r => r.AddAsync(aluno))
                .ReturnsAsync(aluno);

            // Act
            var result = await _service.AddAsync(aluno);

            // Assert
            Assert.True(result.Success);
            _mockRepository.Verify(r => r.AddAsync(aluno), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_WithValidId_ReturnsAluno()
        {
            // Arrange
            var aluno = new AlunoModel { Id = 1, Nome = "João Silva" };
            _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(aluno);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(aluno.Id, result.Id);
        }

        [Fact]
        public async Task DeleteAsync_WithValidId_ReturnsTrue()
        {
            // Arrange
            _mockRepository.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _service.DeleteAsync(1);

            // Assert
            Assert.True(result);
        }
    }
}