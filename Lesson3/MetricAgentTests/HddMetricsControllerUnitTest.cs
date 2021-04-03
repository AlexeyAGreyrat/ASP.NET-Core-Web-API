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
    public class HddMetricsControllerUnitTest
    {
        private Mock<ILogger<HddMetricsController>> mockLogger;
        private Mock<IHddMetricsRepository> mockRepository;
        private HddMetricsController controller;

        public HddMetricsControllerUnitTest()
        {
            mockRepository = new Mock<IHddMetricsRepository>();
            mockLogger = new Mock<ILogger<HddMetricsController>>();
            controller = new HddMetricsController(mockLogger.Object, mockRepository.Object);
        }

        [Fact]
        public void GetSpaceLeft_ReturnsOk()
        {
            //Act
            var result = controller.GetHdd();

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}