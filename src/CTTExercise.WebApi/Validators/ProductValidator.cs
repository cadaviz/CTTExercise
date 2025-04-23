namespace CTTExercise.WebApi.Validators
{
    using CTTExercise.WebApi.Requests;

    public static class ProductValidator
    {
        public static ValidationResult Validate(this RegisterProductRequest request)
        {
            var validationResult = new ValidationResult();

            if (request is null)
            {
                validationResult.Errors.Add("Request cannot be null.");
                return validationResult;
            }
            if (request.Stock < 0)
            {
                validationResult.Errors.Add("Stock cannot be negative.");
            }

            if (string.IsNullOrWhiteSpace(request.Description))
            {
                validationResult.Errors.Add("Description cannot be empty.");
            }

            ValidateCategories(request, validationResult);
          
            if (request.Price <= 0)
            {
                validationResult.Errors.Add("Price must be greater than zero.");
            }
           
            return validationResult;
        }

        private static void ValidateCategories(RegisterProductRequest request, ValidationResult validationResult)
        {
            if (request.Categories.IsNullOrEmpty())
            {
                validationResult.Errors.Add("At least one category is required.");
            }
            else
            {
                if (request.Categories.Any(c => c == Guid.Empty))
                {
                    validationResult.Errors.Add("Category ID cannot be empty.");
                }
                if (request.Categories.Distinct().Count() != request.Categories.Count)
                {
                    validationResult.Errors.Add("Category IDs must be unique.");
                }
            }
        }
    }
}
