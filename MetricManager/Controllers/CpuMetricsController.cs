using MetricManager.DAL.DTO;
using AutoMapper;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricManager.DAL.Client;
using MetricManager.DAL.Metrics;
using Core.Enum;
using MetricManager.DAL.Responses;
using MetricManager.DAL.Models;
using MetricManager.DAL.Request;
using System.Net.Http;

namespace MetricManager.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        private IMetricsAgentClient _metricsAgentClient;
        private IRepositoryGet<CpuMetric> _repository;
        private IMapper _mapper;
        private IAgentsRepository<AgentInfo> _agent;
        private readonly HttpClient _httpClient;
        private readonly ILogger<MetricsAgentClient> _loggerClient;

        public CpuMetricsController(ILogger<CpuMetricsController> logger, IMetricsAgentClient metricsAgentClient,IRepositoryGet<CpuMetric> repository, IMapper mapper, IAgentsRepository<AgentInfo> agent, HttpClient httpClient, ILogger<MetricsAgentClient> loggerClient)
        {
            _agent = agent;
            _mapper = mapper;
            _repository = repository;
            _metricsAgentClient = metricsAgentClient;
            _logger = logger;
            _logger.LogInformation("NLog встроен в CpuMetricsController");
            _httpClient = httpClient;
            _loggerClient = loggerClient;

        }
        //[HttpGet("agenit/{agentId}/from/{fromTime}/to/{toTime}")]
        //public IActionResult GetMetricsFromAgent1([FromRoute] int agentId, [FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        //{
        //    // логируем, что мы пошли в соседний сервис
        //    _logger.LogInformation($"starting new request to metrics agent");
        //    // обращение в сервис
        //    var metrics = new MetricsAgentClient(_httpClient, _loggerClient);
        //        metrics.GetAllCpuMetrics(new GetAllCpuMetricsApiRequest
        //        {
        //        FromTime = fromTime,
        //        ToTime = toTime
        //        });

        //    // возвращаем ответ
        //    return Ok(metrics);
        //}

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Входные данные {agentId} {fromTime} {toTime}");

            var metrics = _repository.GetFromToByAgent(agentId, fromTime, toTime);


            if (metrics == null)
            {
                return Ok();
            }

            var response = new AllCpuMetricsApiResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<CpuMetricDto>(metric));
            }

            return Ok(response);
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsByPercentileFromAgent([FromRoute] int agentId, [FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime,
            [FromRoute] Percentile percentile)
        {
            _logger.LogInformation($"Входные данные {agentId} {fromTime} {toTime} {percentile}");

            var metrics = _repository.GetFromToByAgent(agentId, fromTime, toTime);

            if (metrics == null)
            {
                return Ok();
            }

            var response = new AllCpuMetricsApiResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<CpuMetricDto>(metric));
            }

            return Ok($"По перцентилю {percentile} нагрузка не превышает {metrics.Max(metric => metric.Value)}%");
        }            
    }
}

