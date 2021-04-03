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

    [Route("api/metrics/Ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;
        private readonly IRamMetricsRepository _repository;

        public RamMetricsController(ILogger<RamMetricsController> logger, IRamMetricsRepository repository)
        {
            _repository = repository;
            _logger = logger;
            _logger.LogInformation("NLog встроен в RamMetricsController");
        }
        [HttpGet("available")]
        public IActionResult GetRam()
        {
            _logger.LogInformation("NetworkMetricsController вызов метода GetRam");

            return Ok();
        }
    }
}
