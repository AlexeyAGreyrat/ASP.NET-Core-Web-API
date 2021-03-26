using Lab1.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MetricsAgentTest
{
    public class RamMetricsControllerUnitTest
    {
        private RamMetricsController _controller;
        public RamMetricsControllerUnitTest()
        {
            _controller = new RamMetricsController();
        }

        [Fact]
        public void GetAvailableRam_ReturnsOk()
        {
            //Act
            var result = _controller.GetRam();

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}