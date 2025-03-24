using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AccruentInventoryControl.Domain.ApiModels.Request;
using AccruentInventoryControl.Domain.Constants;
using Xunit;

namespace AccruentInventoryControl.Tests.Domain.ApiModels.Request
{
    public class WarehouseTransactionRequestTests
    {
        [Fact]
        public void WarehouseTransactionRequest_ShouldHaveRequiredProductCode()
        {
            // Arrange
            var request = new WarehouseTransactionRequest();

            // Act
            var validationResults = ValidateModel(request);

            // Assert
            var productCodeError = Assert.Single(validationResults, v => v.MemberNames.Contains(nameof(WarehouseTransactionRequest.ProductCode)));
            Assert.Equal(ErrorKeys.ProductCodeRequired, productCodeError.ErrorMessage);
        }

        [Fact]
        public void WarehouseTransactionRequest_ShouldHaveMaxLengthForProductCode()
        {
            // Arrange
            var request = new WarehouseTransactionRequest
            {
                ProductCode = new string('A', 256) // Exceeding max length
            };

            // Act
            var validationResults = ValidateModel(request);

            // Assert
            var productCodeError = Assert.Single(validationResults, v => v.MemberNames.Contains(nameof(WarehouseTransactionRequest.ProductCode)));
            Assert.Equal(ErrorKeys.ProductCodeMaxLength, productCodeError.ErrorMessage);
        }

        [Fact]
        public void WarehouseTransactionRequest_ShouldHaveRequiredType()
        {
            // Arrange
            var request = new WarehouseTransactionRequest();

            // Act
            var validationResults = ValidateModel(request);

            // Assert
            var typeError = Assert.Single(validationResults, v => v.MemberNames.Contains(nameof(WarehouseTransactionRequest.Type)));
            Assert.Equal(ErrorKeys.WarehouseTransactionTransactionTypeRequired, typeError.ErrorMessage);
        }

        [Fact]
        public void WarehouseTransactionRequest_ShouldHaveValidType()
        {
            // Arrange
            var request = new WarehouseTransactionRequest
            {
                Type = "invalidType"
            };

            // Act
            var validationResults = ValidateModel(request);

            // Assert
            var typeError = Assert.Single(validationResults, v => v.MemberNames.Contains(nameof(WarehouseTransactionRequest.Type)));
            Assert.Equal(ErrorKeys.WarehouseTransactionTransactionTypeInvalid, typeError.ErrorMessage);
        }

        [Fact]
        public void WarehouseTransactionRequest_ShouldHaveValidQuantity()
        {
            // Arrange
            var request = new WarehouseTransactionRequest
            {
                Quantity = 0 // Invalid quantity
            };

            // Act
            var validationResults = ValidateModel(request);

            // Assert
            var quantityError = Assert.Single(validationResults, v => v.MemberNames.Contains(nameof(WarehouseTransactionRequest.Quantity)));
            Assert.Equal(ErrorKeys.WarehouseTransactionQuantityinvalid, quantityError.ErrorMessage);
        }

        [Fact]
        public void WarehouseTransactionRequest_ShouldBeValid()
        {
            // Arrange
            var request = new WarehouseTransactionRequest
            {
                ProductCode = "P12345",
                Type = "inbound",
                Quantity = 10
            };

            // Act
            var validationResults = ValidateModel(request);

            // Assert
            Assert.Empty(validationResults);
        }

        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }
    }
}
