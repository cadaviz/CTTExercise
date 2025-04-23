namespace CTTExercise.WebApi.Responses
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public record RegisterProductResponse(Guid Id, int Stock, string Description, List<Guid> Categories, decimal Price) { }
}
