using AccruentInventoryControl.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AccruentInventoryControl.API.Controllers.v1
{
    /// <summary>
    /// Controller responsible for initializing the in-memory database.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class InMemoryDatabaseInitializerController : ControllerBase
    {
        private readonly ILogger<InMemoryDatabaseInitializerController> _logger;
        private IInMemoryDataBaseInitializerService _inMemoryDataBaseInitializerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryDatabaseInitializerController"/> class.
        /// </summary>
        /// <param name="logger">The logger instance for logging information and errors.</param>
        /// <param name="inMemoryDataBaseInitializerService">The service responsible for initializing the in-memory database.</param>
        public InMemoryDatabaseInitializerController(
            ILogger<InMemoryDatabaseInitializerController> logger,
            IInMemoryDataBaseInitializerService inMemoryDataBaseInitializerService)
        {
            _logger = logger;
            _inMemoryDataBaseInitializerService = inMemoryDataBaseInitializerService;
        }

        /// <summary>
        /// Initializes the in-memory database if it has not already been initialized.
        /// </summary>
        /// <returns>
        /// An <see cref="ActionResult"/> indicating the result of the operation. 
        /// Returns "Database already initialized" if the database is already initialized, 
        /// or "Database initialized" if the initialization is successful.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                if (_inMemoryDataBaseInitializerService.HasInitilized())
                    return Ok("Database already initialized");

                _logger.LogInformation("Initializing in memory database");
                var initialized = await _inMemoryDataBaseInitializerService.Initialize();

                return Ok("Database initialized");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error initializing in memory database");
                return Problem(ex.Message);
            }
        }
    }
}
