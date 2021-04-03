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
    public class NetworkMetricsControllerUnitTest
    {
        private Mock<ILogger<NetworkMetricsController>> _mockLogger;
        private Mock<INetworkMetricsRepository> _mockRepository;
        private NetworkMetricsController _controller;

        public NetworkMetricsControllerUnitTest()
        {
            _mockRepository = new Mock<INetworkMetricsRepository>();
            _mockLogger = new Mock<ILogger<NetworkMetricsController>>();
            _controller = new NetworkMetricsController(_mockLogger.Object, _mockRepository.Object);
        }

        [Fact]
        public void GetMetrics_ReturnsOk()
        {
            //Arrange
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            //Act
            var result = _controller.GetMetrics(fromTime, toTime);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}