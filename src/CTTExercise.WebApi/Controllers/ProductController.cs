namespace CTTExercise.WebApi.Controllers
{
    using CTTExercise.Domain.Services;
    using CTTExercise.Shared.Extensions;
    using CTTExercise.WebApi.Mappers;
    using CTTExercise.WebApi.Requests;
    using CTTExercise.WebApi.Validators;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class ProductController : CTTExercise.WebApi.Controllers.ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService) : base(logger)
        {
            _productService = productService;
        }

        /// <summary>
        /// Get an Order
        /// </summary>
        /// <param name="id" example="ef310f03-b3ce-45ef-b6e3-dd641840fb90">Product identifier</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>Requested order</returns>
        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<IActionResult> GetProductById(Guid id, CancellationToken cancellationToken)
        {
            var product = await _productService.GetProductByIdAsync(id, cancellationToken);

            _logger.LogDebugIfEnabled("Received this order from service. Product={Product}", product!);

            return OkOrNotFound(product.MapToGetProductResponse());
        }

        /// <summary>
        /// Create a new order
        /// </summary>
        /// <param name="request">The new product request body</param>
        /// <param name="cancellationToken">The cancellation token</param>
        [HttpPost(Name = "RegisterProduct")]
        public async Task<IActionResult> RegisterProduct([FromBody] RegisterProductRequest request, CancellationToken cancellationToken)
        {
            _logger.LogDebugIfEnabled("Received request to register product. Request='{Request}'", request);

            var validationResult = request.Validate();

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult);
            }

            var product = request.MapToDomain();

            product = await _productService.CreateProductAsync(product, cancellationToken);

            _logger.LogDebugIfEnabled("Received this product from service. Product='{Product}'", product);

            var response = product.MapToRegisterProductResponse();

            _logger.LogDebugIfEnabled("Product mapped to response. Response='{Response}'", response);

            return CreatedAtAction(nameof(GetProductById), new { id = response.Id }, response);
        }
    }
}
