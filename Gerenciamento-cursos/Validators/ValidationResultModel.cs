namespace Gerenciamento_cursos.Validators
{
    public class ValidationResultModel
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public List<string> Errors { get; set; } = new();

        public static ValidationResultModel SuccessResult() => new() { Success = true };
        public static ValidationResultModel FailureResult(string message) => 
            new() { Success = false, ErrorMessage = message };
        public static ValidationResultModel FailureResult(List<string> errors) => 
            new() { Success = false, Errors = errors };
    }
}