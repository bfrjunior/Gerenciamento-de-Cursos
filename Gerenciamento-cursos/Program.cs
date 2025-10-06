using Gerenciamento_cursos.Data;
using Gerenciamento_cursos.Services.Aluno;
using Gerenciamento_cursos.Services.Cursos;
using Gerenciamento_cursos.Services.Matriculas;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAlunoService, AlunoService>();
builder.Services.AddScoped<ICursoService, CursoService>();
builder.Services.AddScoped<IMatriculaService, MatriculaService>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
