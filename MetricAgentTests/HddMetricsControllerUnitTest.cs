using Lab1.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MetricsAgentTest
{
    public class HddMetricsControllerUnitTest
    {
        private HddMetricsController _controller;

        public HddMetricsControllerUnitTest()
        {
            _controller = new HddMetricsController();
        }

        [Fact]
        public void GetHdd_ReturnsOk()
        {
            //Act
            var result = _controller.GetHdd();

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}