using AccruentInventoryControl.Application.Services.Interfaces;
using AccruentInventoryControl.Domain.ApiModels.Response;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AccruentInventoryControl.API.Controllers.v1
{
    /// <summary>
    /// Controller responsible for handling product-related operations.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private IProductService _productService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="logger">The logger instance for logging information.</param>
        /// <param name="productService">The service responsible for product operations.</param>
        /// <param name="mapper">The mapper instance for object mapping.</param>
        public ProductController(
            ILogger<ProductController> logger,
            IProductService productService,
            IMapper mapper)
        {
            _logger = logger;
            _productService = productService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>
        /// A list of products wrapped in a <see cref="ProductResponse"/> object.
        /// Returns a 204 No Content status if no products are found.
        /// Returns a 500 Internal Server Error status if an exception occurs.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<List<ProductResponse>>> Get()
        {
            try
            {
                var products = await _productService.GetAllProducts();

                if (products.Count() == 0)
                    return NoContent();

                var response = new List<ProductResponse>();

                foreach (var product in products)
                {
                    response.Add(_mapper.Map<ProductResponse>(product));
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
