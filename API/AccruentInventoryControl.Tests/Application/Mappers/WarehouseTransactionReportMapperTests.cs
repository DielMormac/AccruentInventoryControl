using System;
using System.Collections.Generic;
using AutoMapper;
using Xunit;
using AccruentInventoryControl.Application.Mappers;
using AccruentInventoryControl.Domain.ApiModels.Response;
using AccruentInventoryControl.Domain.Entities;

namespace AccruentInventoryControl.Tests.Application.Mappers
{
    public class WarehouseTransactionReportMapperTests
    {
        private readonly IMapper _mapper;

        public WarehouseTransactionReportMapperTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<WarehouseTransactionReportMapper>();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void WarehouseTransactionReportMapper_ShouldMapWarehouseTransactionReportToWarehouseTransactionReportResponse()
        {
            // Arrange
            var report = new WarehouseTransactionReport
            {
                ProductCode = "P12345",
                Date = DateTime.Now,
                WarehouseTransactions = new List<WarehouseTransaction>
                {
                    new WarehouseTransaction { Id = 1, Quantity = 10 },
                    new WarehouseTransaction { Id = 2, Quantity = 20 }
                },
                Balance = 100
            };

            // Act
            var reportResponse = _mapper.Map<WarehouseTransactionReportResponse>(report);

            // Assert
            Assert.Equal(report.ProductCode, reportResponse.ProductCode);
            Assert.Equal(report.Date, reportResponse.Date);
            Assert.Equal(report.Balance, reportResponse.Balance);
            Assert.Equal(report.WarehouseTransactions.Count, reportResponse.WarehouseTransactions.Count);
        }
    }
}
