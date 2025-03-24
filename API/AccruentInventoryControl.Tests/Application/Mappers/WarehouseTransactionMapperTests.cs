using System;
using AutoMapper;
using Xunit;
using AccruentInventoryControl.Application.Mappers;
using AccruentInventoryControl.Domain.ApiModels.Request;
using AccruentInventoryControl.Domain.ApiModels.Response;
using AccruentInventoryControl.Domain.Entities;
using AccruentInventoryControl.Domain.Enums;

namespace AccruentInventoryControl.Tests.Application.Mappers
{
    public class WarehouseTransactionMapperTests
    {
        private readonly IMapper _mapper;

        public WarehouseTransactionMapperTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<WarehouseTransactionMapper>();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void WarehouseTransactionMapper_ShouldMapWarehouseTransactionToWarehouseTransactionResponse()
        {
            // Arrange
            var transaction = new WarehouseTransaction
            {
                Id = 1,
                Quantity = 10,
                Type = WarehouseTransactionType.Inbound,
                Status = WarehouseTransactionStatus.Completed,
                CreatedAt = DateTime.Now,
                PreviousQuantity = 5,
                TotalQuantity = 15
            };

            // Act
            var transactionResponse = _mapper.Map<WarehouseTransactionResponse>(transaction);

            // Assert
            Assert.Equal(transaction.Quantity, transactionResponse.Quantity);
            Assert.Equal("Inbound", transactionResponse.Type);
            Assert.Equal("Completed", transactionResponse.Status);
            Assert.Equal(transaction.CreatedAt, transactionResponse.Date);
            Assert.Equal(transaction.PreviousQuantity, transactionResponse.PreviousQuantity);
            Assert.Equal(transaction.TotalQuantity, transactionResponse.TotalQuantity);
        }

        [Fact]
        public void WarehouseTransactionMapper_ShouldMapWarehouseTransactionRequestToWarehouseTransaction()
        {
            // Arrange
            var transactionRequest = new WarehouseTransactionRequest
            {
                ProductCode = "P12345",
                Quantity = 10,
                Type = "inbound"
            };

            // Act
            var transaction = _mapper.Map<WarehouseTransaction>(transactionRequest);

            // Assert
            Assert.Equal(transactionRequest.ProductCode, transaction.Product.Code);
            Assert.Equal(transactionRequest.Quantity, transaction.Quantity);
            Assert.Equal(WarehouseTransactionType.Inbound, transaction.Type);
            Assert.Equal(WarehouseTransactionStatus.Pending, transaction.Status);
        }
    }
}
