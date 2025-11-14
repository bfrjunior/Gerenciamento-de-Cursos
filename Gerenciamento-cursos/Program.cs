using Gerenciamento_cursos.Data;
using Gerenciamento_cursos.Mappings;
using Gerenciamento_cursos.Middleware;
using Gerenciamento_cursos.Repositories;
using Gerenciamento_cursos.Services.Aluno;
using Gerenciamento_cursos.Services.Cursos;
using Gerenciamento_cursos.Services.Matriculas;
using Gerenciamento_cursos.Validators;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddControllers();
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

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IMatriculaRepository, MatriculaRepository>();

builder.Services.AddScoped<IAlunoValidator, AlunoValidator>();
builder.Services.AddScoped<ICursoValidator, CursoValidator>();

builder.Services.AddScoped<IAlunoService, AlunoService>();
builder.Services.AddScoped<ICursoService, CursoService>();
builder.Services.AddScoped<IMatriculaService, MatriculaService>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("MatriculasEmMemoria");
});

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

// Configure the HTTP request pipeline
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
