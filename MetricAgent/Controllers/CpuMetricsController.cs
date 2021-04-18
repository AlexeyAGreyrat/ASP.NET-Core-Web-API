using Core.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Data.SQLite;
using AutoMapper;
using Core.Interfaces;
using MetricAgent.DAL.Metric;
using MetricAgent.DAL.Models;
using MetricAgent.DAL.Responses;

namespace MetricAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        private IRepository<CpuMetric> _repository;
        private IMapper _mapper;

        public CpuMetricsController(ILogger<CpuMetricsController> logger, IRepository<CpuMetric> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
        }

        [HttpGet("read")]
        public void Test()
        {

        }

        [HttpGet("from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsByPercentileFromAgent([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime,
            [FromRoute] Percentile percentile)
        {
            _logger.LogInformation($"Входные данные {fromTime} {toTime} {percentile}");

            var metrics = _repository.GetFromTo(fromTime, toTime);

            if (metrics == null)
            {
                return Ok();
            }

            var response = new AllCpuMetricsResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };           

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<CpuMetricDto>(metric));
            }

            return Ok($"По перцентилю {percentile} нагрузка не превышает {metrics.Max(metric => metric.Value)}%");
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Входные данные {fromTime} {toTime}");

            var metrics = _repository.GetFromTo(fromTime, toTime);

            if (metrics == null)
            {
                return Ok();
            }

            var response = new AllCpuMetricsResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<CpuMetricDto>(metric));
            }

            return Ok(response);
        }        
    }
} 

