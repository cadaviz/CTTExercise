namespace CTTExercise.WebApi.Requests
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public record RegisterProductRequest(int Stock, string Description, List<Guid> Categories, decimal Price) { }
}
