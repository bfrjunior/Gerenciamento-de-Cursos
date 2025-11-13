using Xunit;
using Gerenciamento_cursos.Validators;
using Gerenciamento_cursos.Model;

namespace Gerenciamento_cursos.Tests
{
    public class CursoValidatorTests
    {
        private readonly CursoValidator _validator;

        public CursoValidatorTests()
        {
            _validator = new CursoValidator();
        }

        [Fact]
        public void Validate_WithValidCurso_ReturnsSuccess()
        {
            // Arrange
            var curso = new CursoModel
            {
                Nome = "C# Avançado",
                Descricao = "Curso completo de C# com padrões de design e boas práticas"
            };

            // Act
            var result = _validator.Validate(curso);

            // Assert
            Assert.True(result.Success);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public void Validate_WithNomeTooShort_ReturnsFailed()
        {
            // Arrange
            var curso = new CursoModel
            {
                Nome = "C#",
                Descricao = "Descrição válida do curso com mais de 10 caracteres"
            };

            // Act
            var result = _validator.Validate(curso);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("O nome do curso deve ter entre 3 e 100 caracteres.", result.Errors);
        }

        [Fact]
        public void Validate_WithDescricaoTooShort_ReturnsFailed()
        {
            // Arrange
            var curso = new CursoModel
            {
                Nome = "C# Avançado",
                Descricao = "Curso"
            };

            // Act
            var result = _validator.Validate(curso);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("A descrição deve ter entre 10 e 500 caracteres.", result.Errors);
        }

        [Fact]
        public void Validate_WithNomeTooLong_ReturnsFailed()
        {
            // Arrange
            var curso = new CursoModel
            {
                Nome = new string('A', 101),
                Descricao = "Descrição válida do curso com mais de 10 caracteres"
            };

            // Act
            var result = _validator.Validate(curso);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("O nome do curso deve ter entre 3 e 100 caracteres.", result.Errors);
        }

        [Fact]
        public void Validate_WithDescricaoTooLong_ReturnsFailed()
        {
            // Arrange
            var curso = new CursoModel
            {
                Nome = "C# Avançado",
                Descricao = new string('A', 501)
            };

            // Act
            var result = _validator.Validate(curso);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("A descrição deve ter entre 10 e 500 caracteres.", result.Errors);
        }

        [Fact]
        public void Validate_WithNomeAndDescricaoEmpty_ReturnsFailed()
        {
            // Arrange
            var curso = new CursoModel
            {
                Nome = "",
                Descricao = ""
            };

            // Act
            var result = _validator.Validate(curso);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(2, result.Errors.Count);
        }
    }
}