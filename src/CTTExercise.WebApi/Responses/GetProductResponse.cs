namespace CTTExercise.WebApi.Responses
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public record GetProductResponse(Guid Id, int Stock, string Description, List<Guid> Categories, decimal Price) { }
}
