using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Management;
using MetricAgent.Metric;
using MetricAgent.Responses;
using AutoMapper;
using MetricAgent;
using MetricAgent.Interface;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;
        private IRepository<RamMetric> _repository;
        private IMapper _mapper;

        public RamMetricsController (ILogger<RamMetricsController> logger, IRepository<RamMetric> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
            _logger.LogDebug("NLog встроен в RamMetricsController");
        }
        [HttpGet("available")]
        public IActionResult GetRam()
        {
            _logger.LogInformation("NetworkMetricsController вызов метода GetRam");

            return Ok();
        }
    }
}
