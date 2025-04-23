namespace CTTExercise.WebApi.Controllers
{
    using CTTExercise.WebApi.Validators;
    using Microsoft.AspNetCore.Mvc;
    using Mvc = Microsoft.AspNetCore.Mvc;
    public class ControllerBase : Mvc.ControllerBase
    {
        protected readonly ILogger<ControllerBase> _logger;

        public ControllerBase(ILogger<ControllerBase> logger)
        {
            _logger = logger;
        }

        protected IActionResult OkOrNotFound(object? response, string notFoundDetail = "No resource found.")
        {
            if (response is null)
            {
                var notFoundResponse = new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Detail = notFoundDetail,
                };

                return NotFound(notFoundResponse);
            }

            return Ok(response);
        }

        protected IActionResult BadRequest(ValidationResult validationResult)
        {
            var problemDetails = new ValidationProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "One or more validation errors occurred.",
                Errors = new Dictionary<string, string[]>() {
                    { "ValidationErrors", validationResult.Errors.ToArray() }
                },
            };

            return BadRequest(problemDetails);
        }
    }
}
