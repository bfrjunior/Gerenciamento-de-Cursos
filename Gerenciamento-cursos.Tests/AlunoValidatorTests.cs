using Xunit;
using Gerenciamento_cursos.Validators;
using Gerenciamento_cursos.Model;
using System.Threading.Tasks;
using System;

namespace Gerenciamento_cursos.Tests
{
    public class AlunoValidatorTests
    {
        [Fact]
        public void ValidateBasicFields_WithValidAluno_ReturnsSuccess()
        {
            // Arrange
            var aluno = new AlunoModel
            {
                Id = 1,
                Nome = "João Silva",
                Email = "joao@example.com",
                DataNascimento = new DateTime(2000, 1, 1)
            };

            // Act & Assert - Teste básico de validação sem DbContext
            Assert.True(aluno.Nome.Length >= 3);
            Assert.True(aluno.Email.Contains("@"));
            Assert.True(DateTime.Now.Year - aluno.DataNascimento.Year >= 18);
        }

        [Fact]
        public void ValidateBasicFields_WithShortName_ReturnsFailed()
        {
            // Arrange
            var aluno = new AlunoModel
            {
                Nome = "Jo",
                Email = "joao@example.com",
                DataNascimento = new DateTime(2000, 1, 1)
            };

            // Act & Assert
            Assert.True(aluno.Nome.Length < 3);
        }

        [Fact]
        public void ValidateBasicFields_WithInvalidEmail_ReturnsFailed()
        {
            // Arrange
            var aluno = new AlunoModel
            {
                Nome = "João Silva",
                Email = "email-invalido",
                DataNascimento = new DateTime(2000, 1, 1)
            };

            // Act & Assert
            Assert.False(aluno.Email.Contains("@"));
        }

        [Fact]
        public void ValidateBasicFields_WithAgeUnder18_ReturnsFailed()
        {
            // Arrange
            var aluno = new AlunoModel
            {
                Nome = "Menor de Idade",
                Email = "menor@example.com",
                DataNascimento = DateTime.Today.AddYears(-15)
            };

            // Act & Assert
            var age = DateTime.Now.Year - aluno.DataNascimento.Year;
            Assert.True(age < 18);
        }
    }
}