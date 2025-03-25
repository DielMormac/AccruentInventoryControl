using AccruentInventoryControl.Application.Services.Interfaces;
using AccruentInventoryControl.Domain.ApiModels.Request;
using AccruentInventoryControl.Domain.ApiModels.Response;
using AccruentInventoryControl.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AccruentInventoryControl.API.Controllers.v1
{
    /// <summary>
    /// Warehouse Transaction Controller
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WarehouseTransactionController : ControllerBase
    {
        private readonly ILogger<WarehouseTransactionController> _logger;
        private readonly IWarehouseTransactionService _warehouseTransactionService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="warehouseTransactionService"></param>
        /// <param name="mapper"></param>
        public WarehouseTransactionController(
            ILogger<WarehouseTransactionController> logger,
            IWarehouseTransactionService warehouseTransactionService,
            IMapper mapper)
        {
            _logger = logger;
            _warehouseTransactionService = warehouseTransactionService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all warehouse transactions
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<WarehouseTransactionResponse>>> Get()
        {
            try
            {
                var transactions = await _warehouseTransactionService.GetAllWarehouseTransactions();

                if (transactions.Count() == 0)
                    return NoContent();


                var response = new List<WarehouseTransactionResponse>();

                foreach (var transaction in transactions)
                {
                    response.Add(_mapper.Map<WarehouseTransactionResponse>(transaction));
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        /// <summary>
        /// Get a warehouse transaction by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<List<WarehouseTransactionResponse>>> Get([FromRoute] long id)
        {
            try
            {
                var transaction = await _warehouseTransactionService.GetWarehouseTransaction(id);

                if (transaction == null)
                    return NoContent();

                var response = _mapper.Map<WarehouseTransactionResponse>(transaction);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// Create a warehouse transaction
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<WarehouseTransactionResponse>> Post([FromBody] WarehouseTransactionRequest request)
        {
            try
            {
                var transaction = _mapper.Map<WarehouseTransaction>(request);

                transaction = await _warehouseTransactionService.CreateWarehouseTransaction(transaction);

                var response = _mapper.Map<WarehouseTransactionResponse>(transaction);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
