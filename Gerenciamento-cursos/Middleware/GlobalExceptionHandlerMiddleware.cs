using System.Net;
using System.Text.Json;
using Gerenciamento_cursos.Common.Result;


namespace Gerenciamento_cursos.Middleware;


public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exceção não tratada capturada pelo middleware global");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        ApiResult response;

        switch (exception)
        {
            case ArgumentNullException:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                response = new ApiResult { Success = false, Message = "Argumento obrigatório não fornecido", Errors = new() };
                break;

            case ArgumentException:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                response = new ApiResult { Success = false, Message = exception.Message, Errors = new() };
                break;

            case UnauthorizedAccessException:
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                response = new ApiResult { Success = false, Message = "Acesso não autorizado", Errors = new() };
                break;

            case KeyNotFoundException:
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                response = new ApiResult { Success = false, Message = "Recurso não encontrado", Errors = new() };
                break;

            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response = new ApiResult { Success = false, Message = "Erro interno do servidor. Por favor, tente novamente mais tarde.", Errors = new() };
                break;
        }

        return context.Response.WriteAsJsonAsync(response);
    }
}