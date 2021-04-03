using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using MetricManager.Enums;
using System.Data.SQLite;
using MetricAgent.DAL;
using MetricAgent.Model;
using MetricAgent.Requests;
using MetricAgent.Responses;
using Microsoft.Extensions.Logging;
using MetricAgent.DTO;
using System.Collections.Generic;

namespace MetricAgent.Controllers
{
    [Route("api/metrics/Networck")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly INetworkMetricsRepository _repository;

        public NetworkMetricsController(ILogger<NetworkMetricsController> logger, INetworkMetricsRepository repository)
        {
            _repository = repository;
            _logger = logger;
            _logger.LogInformation("NLog встроен в NetworkMetricsController");
        }
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("NetworkMetricsController вызов метода GetMetrics");

            var metrics = _repository.GetInTimePeriod(fromTime, toTime);

            var response = new AllNetworkMetricsResponse()
            {
                Metrics = new List<NetworkMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new NetworkMetricDto { Time = metric.Time, Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);
        }
    }
}
