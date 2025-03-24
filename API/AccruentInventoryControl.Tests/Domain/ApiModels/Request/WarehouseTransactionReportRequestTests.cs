using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AccruentInventoryControl.Domain.ApiModels.Request;
using AccruentInventoryControl.Domain.Constants;
using Xunit;

namespace AccruentInventoryControl.Tests.Domain.ApiModels.Request
{
    public class WarehouseTransactionReportRequestTests
    {
        [Fact]
        public void WarehouseTransactionReportRequest_ShouldHaveMaxLengthForProductCode()
        {
            // Arrange
            var request = new WarehouseTransactionReportRequest
            {
                Date = DateTime.Now,
                ProductCode = new string('A', 256) // Exceeding max length
            };

            // Act
            var validationResults = ValidateModel(request);

            // Assert
            var productCodeError = Assert.Single(validationResults, v => v.MemberNames.Contains(nameof(WarehouseTransactionReportRequest.ProductCode)));
            Assert.Equal(ErrorKeys.ProductCodeMaxLength, productCodeError.ErrorMessage);
        }

        [Fact]
        public void WarehouseTransactionReportRequest_ShouldBeValid()
        {
            // Arrange
            var request = new WarehouseTransactionReportRequest
            {
                Date = DateTime.Now,
                ProductCode = "P12345"
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
