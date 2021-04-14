using MetricAgent.Controllers;
using MetricManager.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using MetricAgent.DAL;
using MetricAgent.Model;
using System.Collections.Generic;
using AutoMapper;

namespace MetricsAgentTest
{
    public class CpuMetricsControllerUnitTests
    {
        private CpuMetricsController _controller;
        private Mock<ILogger<CpuMetricsController>> mockLogger;
        private Mock<ICpuMetricsRepository> mockRepository;
        private IMapper mapp;
        public CpuMetricsControllerUnitTests()
        {
            mockRepository = new Mock<ICpuMetricsRepository>();
            mockLogger = new Mock<ILogger<CpuMetricsController>>();
            _controller = new CpuMetricsController(mockLogger.Object, mockRepository.Object, mapp);
        }

       

        [Fact]
        public void GetMetricsByPercentile_ReturnsOk()
        {
            //Arrange
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            var percentile = Percentile.P90;

            //Act
            var result = _controller.GetMetricsByPercentile(fromTime, toTime, percentile);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект
            mockRepository.Setup(repository => repository.Create(It.IsAny<CpuMetric>())).Verifiable();

            // выполняем действие на контроллере
            var result = _controller.Create(new MetricAgent.Requests.CpuMetricCreateRequest
            {
                Time = 1,
                Value = 50
            });

            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            mockRepository.Verify(repository => repository.Create(It.IsAny<CpuMetric>()), Times.AtMostOnce());
        }

        [Fact]
        public void GetAll_ShouldCall_GetAll_From_Repository()
        {
            // Arrange
            mockRepository.Setup(repository => repository.GetAll()).Returns(new List<CpuMetric>());

            // Act
            var result = _controller.GetAll();

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public void GetMetrics_ReturnsOk()
        {
            var fromTime = DateTimeOffset.MinValue;
            var toTime = DateTimeOffset.Now;

            var result = _controller.GetMetrics(fromTime, toTime);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
