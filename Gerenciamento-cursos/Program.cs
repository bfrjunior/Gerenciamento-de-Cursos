using Gerenciamento_cursos.Data;
using Gerenciamento_cursos.Repositories;
using Gerenciamento_cursos.Services.Aluno;
using Gerenciamento_cursos.Services.Cursos;
using Gerenciamento_cursos.Services.Matriculas;
using Gerenciamento_cursos.Validators;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {


            policy.WithOrigins("http://localhost:5173", "https://gerenciamento-matriculas.vercel.app", "http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Registrar repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IMatriculaRepository, MatriculaRepository>();

// Registrar validadores
builder.Services.AddScoped<IAlunoValidator, AlunoValidator>();
builder.Services.AddScoped<ICursoValidator, CursoValidator>();

// Registrar serviços
builder.Services.AddScoped<IAlunoService, AlunoService>();
builder.Services.AddScoped<ICursoService, CursoService>();
builder.Services.AddScoped<IMatriculaService, MatriculaService>();

// Configurar DbContext com banco em memória
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("MatriculasEmMemoria");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();
app.MapControllers();

app.Run();
