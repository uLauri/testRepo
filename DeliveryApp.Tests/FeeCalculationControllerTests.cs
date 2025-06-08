using DeliveryApp.Controllers;
using DeliveryApp.Models.Fees;
using DeliveryApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Tests
{
    public class FeeCalculationControllerTests
    {
        [Fact]
        public async Task CalculateFee_ValidRequest_ShouldReturnOk()
        {
            var mockService = new Mock<IFeeCalculationService>();
            mockService.Setup(service => service.CalculateFee("tallinn", "car"))
                       .ReturnsAsync(15.0); 

            var controller = new FeeCalculationController(mockService.Object);

            var result = await controller.CalculateFee("tallinn", "car");

            var okResult = Assert.IsType<OkObjectResult>(result);
            var responseData = Assert.IsType<FeeResponse>(okResult.Value);
            
            Assert.NotNull(responseData);
            Assert.Equal(15.0, responseData.DeliveryFee);
        }
    }
}
