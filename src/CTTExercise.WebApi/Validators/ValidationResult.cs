namespace CTTExercise.WebApi.Validators
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class ValidationResult
    {
        public bool IsValid => Errors.Count == 0;
        public List<string> Errors { get; } = new();
    }
}
