using Gerenciamento_cursos.Model;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento_cursos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<AlunoModel> Alunos { get; set; }
        public DbSet<CursoModel> Cursos { get; set; }
        public DbSet<MatriculaModel> Matriculas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração da entidade Aluno
            modelBuilder.Entity<AlunoModel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(150);
                entity.Property(e => e.DataNascimento)
                    .IsRequired();
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // Configuração da entidade Curso
            modelBuilder.Entity<CursoModel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            // Configuração da entidade Matrícula
            modelBuilder.Entity<MatriculaModel>(entity =>
            {
                entity.HasKey(m => new { m.AlunoId, m.CursoId });
                entity.Property(e => e.DataMatricula).IsRequired();
                entity.HasOne(m => m.Aluno)
                    .WithMany(a => a.Matriculas)
                    .HasForeignKey(m => m.AlunoId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(m => m.Curso)
                    .WithMany(c => c.Matriculas)
                    .HasForeignKey(m => m.CursoId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
