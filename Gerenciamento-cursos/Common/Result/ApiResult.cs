namespace Gerenciamento_cursos.Common.Result
{
   
    public class ApiResult<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new();

        public static ApiResult<T> SuccessResult(T data, string message = "Operação realizada com sucesso")
            => new ApiResult<T> { Success = true, Data = data, Message = message };

        public static ApiResult<T> FailureResult(string message, List<string>? errors = null)
            => new ApiResult<T> { Success = false, Message = message, Errors = errors ?? new() };

        public static ApiResult<T> FailureResult(List<string> errors)
            => new ApiResult<T> { Success = false, Message = "Erro de validação", Errors = errors };
    }

 
    public class ApiResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new();

        public static ApiResult SuccessResult(string message = "Operação realizada com sucesso")
            => new ApiResult { Success = true, Message = message };

        public static ApiResult FailureResult(string message, List<string>? errors = null)
            => new ApiResult { Success = false, Message = message, Errors = errors ?? new() };

        public static ApiResult FailureResult(List<string> errors)
            => new ApiResult { Success = false, Message = "Erro de validação", Errors = errors };
    }
}