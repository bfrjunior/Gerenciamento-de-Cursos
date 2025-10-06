using Gerenciamento_cursos.Model;
using Microsoft.EntityFrameworkCore;

namespace Gerenciamento_cursos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSets para suas entidades
        public DbSet<AlunoModel> Alunos { get; set; }
        public DbSet<CursoModel> Cursos { get; set; }
        public DbSet<MatriculaModel> Matriculas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<MatriculaModel>()
                .HasKey(m => new { m.AlunoId, m.CursoId });


            modelBuilder.Entity<MatriculaModel>()
                .HasOne(m => m.Aluno)
                .WithMany(a => a.Matriculas)
                .HasForeignKey(m => m.AlunoId);


            modelBuilder.Entity<MatriculaModel>()
                .HasOne(m => m.Curso)
                .WithMany(c => c.Matriculas)
                .HasForeignKey(m => m.CursoId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
