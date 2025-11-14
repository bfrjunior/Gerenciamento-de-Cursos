using Xunit;
using Moq;
using Gerenciamento_cursos.Services.Cursos;
using Gerenciamento_cursos.Repositories;
using Gerenciamento_cursos.Model;
using Gerenciamento_cursos.Validators;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Gerenciamento_cursos.Common.Result;

namespace Gerenciamento_cursos.Tests
{
    public class CursoServiceTests
    {
        private readonly Mock<IRepository<CursoModel>> _mockRepository;
        private readonly Mock<ICursoValidator> _mockValidator;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CursoService _service;

        public CursoServiceTests()
        {
            _mockRepository = new Mock<IRepository<CursoModel>>();
            _mockValidator = new Mock<ICursoValidator>();
            _mockMapper = new Mock<IMapper>();
            _service = new CursoService(_mockRepository.Object, _mockValidator.Object);
        }

        [Fact]
        public async Task AddAsync_WithValidCurso_ReturnsSuccess()
        {
            // Arrange
            var curso = new CursoModel 
            { 
                Nome = "C# Avançado", 
                Descricao = "Curso completo de C# com .NET 8 e padrões de design" 
            };
            
            _mockValidator.Setup(v => v.Validate(curso))
                .Returns(ValidationResultModel.SuccessResult());
            
            _mockRepository.Setup(r => r.AddAsync(curso))
                .ReturnsAsync(curso);

            // Act
            var result = await _service.AddAsync(curso);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            _mockRepository.Verify(r => r.AddAsync(curso), Times.Once);
        }

        [Fact]
        public async Task AddAsync_WithInvalidCurso_ReturnsFailed()
        {
            // Arrange
            var curso = new CursoModel 
            { 
                Nome = "C#",
                Descricao = "Curso" 
            };
            
            var validationResult = ValidationResultModel.FailureResult(
                new List<string> { "O nome do curso deve ter entre 3 e 100 caracteres." });
            
            _mockValidator.Setup(v => v.Validate(curso))
                .Returns(validationResult);

            // Act
            var result = await _service.AddAsync(curso);

            // Assert
            Assert.False(result.Success);
            _mockRepository.Verify(r => r.AddAsync(It.IsAny<CursoModel>()), Times.Never);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllCursos()
        {
            // Arrange
            var cursos = new List<CursoModel>
            {
                new CursoModel { Id = 1, Nome = "C#", Descricao = "Linguagem de programação moderna" },
                new CursoModel { Id = 2, Nome = "ASP.NET", Descricao = "Framework para desenvolvimento web" }
            };

            _mockRepository.Setup(r => r.GetAllAsync())
                .ReturnsAsync(cursos);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.True(result.Success);
            Assert.Equal(2, result.Data.Count());
        }

        [Fact]
        public async Task GetByIdAsync_WithValidId_ReturnsCurso()
        {
            // Arrange
            var curso = new CursoModel 
            { 
                Id = 1, 
                Nome = "C# Avançado", 
                Descricao = "Curso completo de C#" 
            };

            _mockRepository.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(curso);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task GetByIdAsync_WithInvalidId_ReturnsNull()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetByIdAsync(999))
                .ReturnsAsync((CursoModel?)null);

            // Act
            var result = await _service.GetByIdAsync(999);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task UpdateAsync_WithValidCurso_ReturnsSuccess()
        {
            // Arrange
            var cursoExistente = new CursoModel 
            { 
                Id = 1, 
                Nome = "C#", 
                Descricao = "Curso básico" 
            };

            var cursoAtualizado = new CursoModel 
            { 
                Id = 1, 
                Nome = "C# Avançado", 
                Descricao = "Curso completo de C# com padrões avançados" 
            };

            _mockRepository.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(cursoExistente);

            _mockValidator.Setup(v => v.Validate(It.IsAny<CursoModel>()))
                .Returns(ValidationResultModel.SuccessResult());

            _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<CursoModel>()))
                .ReturnsAsync(true);

            // Act
            var result = await _service.UpdateAsync(cursoAtualizado);

            // Assert
            Assert.True(result.Success);
            _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<CursoModel>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_WithNonExistentCurso_ReturnsFailed()
        {
            // Arrange
            var curso = new CursoModel { Id = 999, Nome = "Inexistente", Descricao = "Este curso não existe" };

            _mockRepository.Setup(r => r.GetByIdAsync(999))
                .ReturnsAsync((CursoModel?)null);

            // Act
            var result = await _service.UpdateAsync(curso);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task DeleteAsync_WithValidId_ReturnsTrue()
        {
            // Arrange
            _mockRepository.Setup(r => r.DeleteAsync(1))
                .ReturnsAsync(true);

            // Act
            var result = await _service.DeleteAsync(1);

            // Assert
            Assert.True(result.Success);
            _mockRepository.Verify(r => r.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WithInvalidId_ReturnsFalse()
        {
            // Arrange
            _mockRepository.Setup(r => r.DeleteAsync(999))
                .ReturnsAsync(false);

            // Act
            var result = await _service.DeleteAsync(999);

            // Assert
            Assert.False(result.Success);
        }
    }
}