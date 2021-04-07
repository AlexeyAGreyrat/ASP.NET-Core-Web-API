using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SQLite;
using MetricAgent.DAL;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace MetricAgent.Controllers
{

    [Route("api/metrics/Ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;
        private readonly IRamMetricsRepository _repository;
        private readonly IMapper _mapper;

        public RamMetricsController(ILogger<RamMetricsController> logger, IRamMetricsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
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
