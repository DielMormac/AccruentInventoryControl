using AccruentInventoryControl.Application.Services.Interfaces;
using AccruentInventoryControl.Domain.Entities;
using AccruentInventoryControl.Infrastructure.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AccruentInventoryControl.Application.Services
{
    public class InMemoryDataBaseInitializerService : IInMemoryDataBaseInitializerService
    {
        private readonly ILogger<InMemoryDataBaseInitializerService> _logger;
        private readonly IProductRepository _productRepository;
        private readonly IWarehouseTransactionRepository _warehouseTransactionRepository;
        private readonly IWarehouseTransactionService _warehouseTransactionService;
        private readonly IConfiguration _config;
        private bool _initialized = false;
        private bool _disposedValue = false;
        private Random Randomizer;

        public InMemoryDataBaseInitializerService(
            ILogger<InMemoryDataBaseInitializerService> logger,
            IProductRepository productRepository,
            IWarehouseTransactionRepository warehouseTransactionRepository,
            IWarehouseTransactionService warehouseTransactionService,
            IConfiguration config)
        {
            _logger = logger;
            _productRepository = productRepository;
            _warehouseTransactionRepository = warehouseTransactionRepository;
            _warehouseTransactionService = warehouseTransactionService;
            _config = config;
        }

        public bool HasInitilized()
        {
            return _initialized;
        }

        public async Task<bool> Initialize()
        {
            if (_initialized)
                return _initialized;

            Randomizer = new Random();
            var inMemoryProductCount = Convert.ToInt32(_config["InMemoryProductCount"]);
            var inMemoryDaysHistory = Convert.ToInt32(_config["InMemoryDaysHistory"]);

            await FirstWarehouseTransactionInbound(inMemoryProductCount, inMemoryDaysHistory);

            _initialized = true;

            return _initialized;
        }

        #region Private Methods

        private async Task FirstWarehouseTransactionInbound(int count, int inMemoryDaysHistory)
        {
            _logger.LogInformation("Generating WarehouseTransactionInbound Database");
            var firstDayInbound = inMemoryDaysHistory * -1;

            var warehouseTransactions = new List<WarehouseTransaction>();
            for (int i = 1; i <= count; i++)
            {
                var quantity = Randomizer.Next(1, 10) * 1000;

                warehouseTransactions.Add(new WarehouseTransaction
                {
                    Product = new Product
                    {
                        Code = $"P{i}",
                        Name = $"Product {i}",
                    },
                    Quantity = quantity,
                    CreatedAt = DateTime.Now.AddDays(firstDayInbound),
                    UpdatedAt = DateTime.Now.AddDays(firstDayInbound),
                    PreviousQuantity = 0,
                    TotalQuantity = quantity,
                    Status = Domain.Enums.WarehouseTransactionStatus.Completed,
                    Type = Domain.Enums.WarehouseTransactionType.Inbound,
                });
            }

            _logger.LogInformation("Inserting first WarehouseTransaction inbound");
            foreach (var warehouseTransaction in warehouseTransactions)
            {
                await _warehouseTransactionRepository.AddAsync(warehouseTransaction);
            }

            InsertRandomWarehouseTransactions(count, firstDayInbound);
        }

        private async Task InsertRandomWarehouseTransactions(int count, int daysInPast)
        {
            _logger.LogInformation("Generating random WarehouseTransactions");
            while (daysInPast <= 0)
            {
                var warehouseTransactions = new List<WarehouseTransaction>();

                for (int i = 1; i <= count; i++)
                {
                    var quantity = Randomizer.Next(0, 10) * 50;
                    var inbound = Randomizer.Next(0, 3) > 0;

                    if (quantity == 0)
                        continue;

                    warehouseTransactions.Add(new WarehouseTransaction
                    {
                        Product = new Product
                        {
                            Code = $"P{i}",
                            Name = $"Product {i}",
                        },
                        Quantity = quantity,
                        CreatedAt = DateTime.Now.AddDays(daysInPast),
                        UpdatedAt = DateTime.Now.AddDays(daysInPast),
                        PreviousQuantity = 0,
                        TotalQuantity = 0,
                        Status = Domain.Enums.WarehouseTransactionStatus.Completed,
                        Type = inbound ? Domain.Enums.WarehouseTransactionType.Inbound
                        : Domain.Enums.WarehouseTransactionType.Outbound,
                    });
                }

                foreach (var warehouseTransaction in warehouseTransactions)
                {
                    await _warehouseTransactionService.CreateWarehouseTransaction(warehouseTransaction);
                }

                daysInPast++;
            }
        }
        #endregion
    }
}
