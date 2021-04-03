using System;
using Xunit;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using MetricAgent.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using MetricAgent.DAL;
using MetricAgent.Model;

namespace MetricsAgentTest
{
    public class RamMetricsControllerUnitTest
    {
        private Mock<ILogger<RamMetricsController>> _mockLogger;
        private Mock<IRamMetricsRepository> _mockRepository;
        private RamMetricsController _controller;

        public RamMetricsControllerUnitTest()
        {
            _mockRepository = new Mock<IRamMetricsRepository>();
            _mockLogger = new Mock<ILogger<RamMetricsController>>();
            _controller = new RamMetricsController(_mockLogger.Object, _mockRepository.Object);
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