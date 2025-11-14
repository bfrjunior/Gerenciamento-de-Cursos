namespace Gerenciamento_cursos.Validators
{
    public class ValidationResultModel
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public List<string> Errors { get; set; } = new();

        public static ValidationResultModel SuccessResult() => new ValidationResultModel { Success = true };
        public static ValidationResultModel FailureResult(string message) => 
            new ValidationResultModel { Success = false, ErrorMessage = message };
        public static ValidationResultModel FailureResult(List<string> errors) => 
            new ValidationResultModel { Success = false, Errors = errors };
    }
}