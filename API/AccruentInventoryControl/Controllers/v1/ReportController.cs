using AccruentInventoryControl.Application.Services.Interfaces;
using AccruentInventoryControl.Domain.ApiModels.Request;
using AccruentInventoryControl.Domain.ApiModels.Response;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AccruentInventoryControl.API.Controllers.v1
{
    /// <summary>
    /// Controller responsible for handling report-related operations.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly ILogger<ReportController> _logger;
        private readonly IWarehouseTransactionReportService _warehouseTransactionReportService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportController"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="warehouseTransactionReportService"></param>
        /// <param name="mapper"></param>
        public ReportController(
            ILogger<ReportController> logger,
            IWarehouseTransactionReportService warehouseTransactionReportService,
            IMapper mapper)
        {
            _logger = logger;
            _warehouseTransactionReportService = warehouseTransactionReportService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves a warehouse transaction report based on the provided date and product code.
        /// </summary>
        /// <param name="request">The request object containing the date and product code for the report.</param>
        /// <returns>
        /// An <see cref="ActionResult"/> containing a list of <see cref="WarehouseTransactionReportResponse"/> objects
        /// if the report is found, or a NoContent response if no report is available.
        /// </returns>
        /// <response code="200">Returns the warehouse transaction report.</response>
        /// <response code="204">No content if the report is not found.</response>
        /// <response code="500">If an exception occurs during processing.</response>
        [HttpPost("WarehouseTransaction")]
        public async Task<ActionResult<List<WarehouseTransactionReportResponse>>> Get([FromBody] WarehouseTransactionReportRequest request)
        {
            try
            {
                var report = await _warehouseTransactionReportService.GetWarehouseTransactionReportByDateAndProduct(request.Date, request.ProductCode);

                if (report == null)
                    return NoContent();


                var response = _mapper.Map<WarehouseTransactionReportResponse>(report);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
