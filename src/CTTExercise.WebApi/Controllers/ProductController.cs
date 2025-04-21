namespace CTTExercise.WebApi.Controllers
{
    using CTTExercise.Domain.Services;
    using CTTExercise.WebApi.Requests;
    using Microsoft.AspNetCore.Mvc;

    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService) : base()
        {
            _logger = logger;
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create a new order
        /// </summary>
        /// <param name="request">The new product request body</param>
        /// <param name="cancellationToken">The cancellation token</param>
        [HttpPost(Name = "RegisterProduct")]
        public async Task<IActionResult> RegisterProduct([FromBody] RegisterProductRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
