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
using AutoMapper;

namespace MetricsAgentTest
{
    public class NetworkMetricsControllerUnitTest
    {
        private Mock<ILogger<NetworkMetricsController>> _mockLogger;
        private Mock<INetworkMetricsRepository> _mockRepository;
        private NetworkMetricsController _controller;
        private IMapper mapp;

        public NetworkMetricsControllerUnitTest()
        {
            _mockRepository = new Mock<INetworkMetricsRepository>();
            _mockLogger = new Mock<ILogger<NetworkMetricsController>>();
            _controller = new NetworkMetricsController(_mockLogger.Object, _mockRepository.Object, mapp);
        }

        [Fact]
        public void GetMetrics_ReturnsOk()
        {
            //Arrange
            var fromTime = DateTimeOffset.MinValue;
            var toTime = DateTimeOffset.Now;

            //Act
            var result = _controller.GetMetrics(fromTime, toTime);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}