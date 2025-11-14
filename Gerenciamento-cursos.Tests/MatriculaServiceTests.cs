using Xunit;
using Moq;
using Gerenciamento_cursos.Services.Matriculas;
using Gerenciamento_cursos.Repositories;
using Gerenciamento_cursos.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using AutoMapper;
using Gerenciamento_cursos.Common.Result;

namespace Gerenciamento_cursos.Tests
{


    public class MatriculaServiceTests
    {
        private readonly Mock<IRepository<AlunoModel>> _mockAlunoRepository;
        private readonly Mock<IRepository<CursoModel>> _mockCursoRepository;
        private readonly Mock<IMatriculaRepository> _mockMatriculaRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly MatriculaService _service;

        public MatriculaServiceTests()
        {
            _mockAlunoRepository = new Mock<IRepository<AlunoModel>>();
            _mockCursoRepository = new Mock<IRepository<CursoModel>>();
            _mockMatriculaRepository = new Mock<IMatriculaRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new MatriculaService(
                _mockAlunoRepository.Object, 
                _mockCursoRepository.Object, 
                _mockMatriculaRepository.Object,
                _mockMapper.Object);
        }

        [Fact]
        public async Task MatricularAsync_WithValidAlunoAndCurso_ReturnsSuccess()
        {
            // Arrange
            int alunoId = 1;
            int cursoId = 1;

            _mockAlunoRepository.Setup(r => r.ExistsAsync(alunoId))
                .ReturnsAsync(true);

            _mockCursoRepository.Setup(r => r.ExistsAsync(cursoId))
                .ReturnsAsync(true);

            _mockMatriculaRepository.Setup(r => r.ExistsAsync(alunoId, cursoId))
                .ReturnsAsync(false);

            _mockMatriculaRepository.Setup(r => r.AddAsync(It.IsAny<MatriculaModel>()))
                .ReturnsAsync(new MatriculaModel { AlunoId = alunoId, CursoId = cursoId });

            // Act
            var result = await _service.MatricularAsync(alunoId, cursoId);

            // Assert
            Assert.True(result.Success);
            _mockMatriculaRepository.Verify(r => r.AddAsync(It.IsAny<MatriculaModel>()), Times.Once);
        }

        [Fact]
        public async Task MatricularAsync_WithNonExistentAluno_ReturnsFailed()
        {
            // Arrange
            int alunoId = 999;
            int cursoId = 1;

            _mockAlunoRepository.Setup(r => r.ExistsAsync(alunoId))
                .ReturnsAsync(false);

            // Act
            var result = await _service.MatricularAsync(alunoId, cursoId);

            // Assert
            Assert.False(result.Success);
            _mockMatriculaRepository.Verify(r => r.AddAsync(It.IsAny<MatriculaModel>()), Times.Never);
        }

        [Fact]
        public async Task MatricularAsync_WithNonExistentCurso_ReturnsFailed()
        {
            // Arrange
            int alunoId = 1;
            int cursoId = 999;

            _mockAlunoRepository.Setup(r => r.ExistsAsync(alunoId))
                .ReturnsAsync(true);

            _mockCursoRepository.Setup(r => r.ExistsAsync(cursoId))
                .ReturnsAsync(false);

            // Act
            var result = await _service.MatricularAsync(alunoId, cursoId);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task MatricularAsync_WithDuplicateMatricula_ReturnsFailed()
        {
            // Arrange
            int alunoId = 1;
            int cursoId = 1;

            _mockAlunoRepository.Setup(r => r.ExistsAsync(alunoId))
                .ReturnsAsync(true);

            _mockCursoRepository.Setup(r => r.ExistsAsync(cursoId))
                .ReturnsAsync(true);

            _mockMatriculaRepository.Setup(r => r.ExistsAsync(alunoId, cursoId))
                .ReturnsAsync(true);

            // Act
            var result = await _service.MatricularAsync(alunoId, cursoId);

            // Assert
            Assert.False(result.Success);
            _mockMatriculaRepository.Verify(r => r.AddAsync(It.IsAny<MatriculaModel>()), Times.Never);
        }

        [Fact]
        public async Task RemoverMatriculaAsync_WithValidIds_ReturnsTrue()
        {
            // Arrange
            int alunoId = 1;
            int cursoId = 1;

            _mockMatriculaRepository.Setup(r => r.DeleteAsync(alunoId, cursoId))
                .ReturnsAsync(true);

            // Act
            var result = await _service.RemoverMatriculaAsync(alunoId, cursoId);

            // Assert
            Assert.True(result.Success);
            _mockMatriculaRepository.Verify(r => r.DeleteAsync(alunoId, cursoId), Times.Once);
        }

        [Fact]
        public async Task RemoverMatriculaAsync_WithInvalidIds_ReturnsFalse()
        {
            // Arrange
            int alunoId = 999;
            int cursoId = 999;

            _mockMatriculaRepository.Setup(r => r.DeleteAsync(alunoId, cursoId))
                .ReturnsAsync(false);

            // Act
            var result = await _service.RemoverMatriculaAsync(alunoId, cursoId);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task GetAlunosByCursoAsync_WithValidCursoId_ReturnsAlunos()
        {
            // Arrange
            int cursoId = 1;
            var alunos = new List<AlunoModel>
            {
                new AlunoModel { Id = 1, Nome = "João", Email = "joao@example.com", DataNascimento = new DateTime(2005, 1, 1) },
                new AlunoModel { Id = 2, Nome = "Maria", Email = "maria@example.com", DataNascimento = new DateTime(2003, 5, 15) }
            };

            _mockMatriculaRepository.Setup(r => r.GetAlunosByCursoAsync(cursoId))
                .ReturnsAsync(alunos);

            // Act
            var result = await _service.GetAlunosByCursoAsync(cursoId);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(2, result.Data.Count());
        }

        [Fact]
        public async Task GetAlunosByCursoAsync_WithNoCursoAlunos_ReturnsEmptyList()
        {
            // Arrange
            int cursoId = 999;

            _mockMatriculaRepository.Setup(r => r.GetAlunosByCursoAsync(cursoId))
                .ReturnsAsync(new List<AlunoModel>());

            // Act
            var result = await _service.GetAlunosByCursoAsync(cursoId);

            // Assert
            Assert.True(result.Success);
            Assert.Empty(result.Data);
        }
    }
}