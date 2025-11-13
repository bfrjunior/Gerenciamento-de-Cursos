using Xunit;
using Moq;
using Gerenciamento_cursos.Validators;
using Gerenciamento_cursos.Model;
using Gerenciamento_cursos.Data;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento_cursos.Tests
{
    public class AlunoValidatorTests
    {
        private readonly AlunoValidator _validator;
        private readonly Mock<AppDbContext> _mockContext;

        public AlunoValidatorTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TestDb_" + Guid.NewGuid())
                .Options;

            _mockContext = new Mock<AppDbContext>(options);
            _validator = new AlunoValidator(_mockContext.Object);
        }

        [Fact]
        public async Task ValidateAsync_WithValidAluno_ReturnsSuccess()
        {
            // Arrange
            var aluno = new AlunoModel
            {
                Id = 1,
                Nome = "João Silva",
                Email = "joao@example.com",
                DataNascimento = new DateTime(2005, 1, 1)
            };

            var dbSetMock = new Mock<DbSet<AlunoModel>>();
            dbSetMock.Setup(d => d.AnyAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<AlunoModel, bool>>>(), default))
                .ReturnsAsync(false);

            _mockContext.Setup(c => c.Alunos).Returns(dbSetMock.Object);

            // Act
            var result = await _validator.ValidateAsync(aluno, isUpdate: false);

            // Assert
            Assert.True(result.Success);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public async Task ValidateAsync_WithNomeTooShort_ReturnsFailed()
        {
            // Arrange
            var aluno = new AlunoModel
            {
                Nome = "Jo",
                Email = "joao@example.com",
                DataNascimento = new DateTime(2005, 1, 1)
            };

            var dbSetMock = new Mock<DbSet<AlunoModel>>();
            _mockContext.Setup(c => c.Alunos).Returns(dbSetMock.Object);

            // Act
            var result = await _validator.ValidateAsync(aluno, isUpdate: false);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("O nome deve ter entre 3 e 100 caracteres.", result.Errors);
        }

        [Fact]
        public async Task ValidateAsync_WithInvalidEmail_ReturnsFailed()
        {
            // Arrange
            var aluno = new AlunoModel
            {
                Nome = "João Silva",
                Email = "email-invalido",
                DataNascimiento = new DateTime(2005, 1, 1)
            };

            var dbSetMock = new Mock<DbSet<AlunoModel>>();
            _mockContext.Setup(c => c.Alunos).Returns(dbSetMock.Object);

            // Act
            var result = await _validator.ValidateAsync(aluno, isUpdate: false);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("O email informado não é válido.", result.Errors);
        }

        [Fact]
        public async Task ValidateAsync_WithAgeUnder18_ReturnsFailed()
        {
            // Arrange
            var aluno = new AlunoModel
            {
                Nome = "Menor de Idade",
                Email = "menor@example.com",
                DataNascimento = DateTime.Today.AddYears(-15)
            };

            var dbSetMock = new Mock<DbSet<AlunoModel>>();
            dbSetMock.Setup(d => d.AnyAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<AlunoModel, bool>>>(), default))
                .ReturnsAsync(false);

            _mockContext.Setup(c => c.Alunos).Returns(dbSetMock.Object);

            // Act
            var result = await _validator.ValidateAsync(aluno, isUpdate: false);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("O aluno deve ter pelo menos 18 anos.", result.Errors);
        }

        [Fact]
        public async Task ValidateAsync_WithDuplicateEmail_ReturnsFailed()
        {
            // Arrange
            var aluno = new AlunoModel
            {
                Id = 1,
                Nome = "João Silva",
                Email = "joao@example.com",
                DataNascimento = new DateTime(2005, 1, 1)
            };

            var dbSetMock = new Mock<DbSet<AlunoModel>>();
            dbSetMock.Setup(d => d.AnyAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<AlunoModel, bool>>>(), default))
                .ReturnsAsync(true);

            _mockContext.Setup(c => c.Alunos).Returns(dbSetMock.Object);

            // Act
            var result = await _validator.ValidateAsync(aluno, isUpdate: false);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Este email já está registrado.", result.Errors);
        }

        [Fact]
        public async Task ValidateAsync_WithEmailTooLong_ReturnsFailed()
        {
            // Arrange
            var longEmail = new string('a', 140) + "@example.com";
            var aluno = new AlunoModel
            {
                Nome = "João Silva",
                Email = longEmail,
                DataNascimento = new DateTime(2005, 1, 1)
            };

            var dbSetMock = new Mock<DbSet<AlunoModel>>();
            _mockContext.Setup(c => c.Alunos).Returns(dbSetMock.Object);

            // Act
            var result = await _validator.ValidateAsync(aluno, isUpdate: false);

            // Assert
            Assert.False(result.Success);
        }
    }
}