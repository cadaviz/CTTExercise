namespace CTTExercise.WebApi.Mappers
{
    using CTTExercise.Domain.Entities;
    using CTTExercise.WebApi.Requests;
    using CTTExercise.WebApi.Responses;

    internal static class ProductMapper
    {
        public static Product MapToDomain(this RegisterProductRequest request)
        {
            return new Product
            {
                Stock = request.Stock,
                Description = request.Description,
                Categories = request.Categories,
                Price = request.Price,
            };
        }

        public static GetProductResponse? MapToGetProductResponse(this Product? product)
        {
            if (product is null)
            {
                return null;
            }

            return new GetProductResponse
            (
                Id: product.Id,
                Stock: product.Stock,
                Description: product.Description,
                Categories: product.Categories,
                Price: product.Price
            );
        }

        public static RegisterProductResponse MapToRegisterProductResponse(this Product product)
        {
            return new RegisterProductResponse
            (
                Id: product.Id,
                Stock: product.Stock,
                Description: product.Description,
                Categories: product.Categories,
                Price: product.Price
            );
        }
    }
}
