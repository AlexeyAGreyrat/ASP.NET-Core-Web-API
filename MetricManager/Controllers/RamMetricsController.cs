using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enum;
using Microsoft.Extensions.Logging;
using MetricManager.DAL.Client;
using AutoMapper;
using Core.Interfaces;
using MetricManager.DAL.DTO;
using MetricManager.DAL.Metrics;
using MetricManager.DAL.Models;
using MetricManager.DAL.Responses;

namespace MetricManager.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]

    public class RamMetricsController : Controller
    {
        private readonly ILogger<RamMetricsController> _logger;
        private IMetricsAgentClient _metricsAgentClient;
        private IRepositoryGet<RamMetric> _repository;
        private IMapper _mapper;
        private IAgentsRepository<AgentInfo> _agent;

        public RamMetricsController(ILogger<RamMetricsController> logger, IMetricsAgentClient metricsAgentClient,
            IRepositoryGet<RamMetric> repository, IMapper mapper, IAgentsRepository<AgentInfo> agent)
        {
            _agent = agent;
            _mapper = mapper;
            _repository = repository;
            _metricsAgentClient = metricsAgentClient;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в RamMetricsController");
        }
        /// <summary>
        /// Получает метрики Ram на заданном диапазоне времени
        /// </summary>
        /// <param name="fromTime">начальная метрка времени в секундах с 01.01.2000</param>
        /// <param name="toTime">конечная метрка времени в секундах с 01.01.2021</param>
        /// <returns>Список метрик, которые были сохранены в заданном диапазоне времени</returns>
        /// <response code="201">Если все хорошо</response>
        /// <response code="400">если передали не правильные параетры</response> 
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId, [FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Входные данные {agentId} {fromTime} {toTime}");

            var metrics = _repository.GetFromToByAgent(agentId, fromTime, toTime);

            if (metrics == null)
            {
                return Ok();
            }

            var response = new AllRamMetricsApiResponse()
            {
                Metrics = new List<RamMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<RamMetricDto>(metric));
            }

            return Ok(response);
        }
        /// <summary>
        /// Получает метрики Ram на заданном диапазоне времени с працентли
        /// </summary>
        /// <param name="fromTime">начальная метрка времени в секундах с 01.01.2000</param>
        /// <param name="toTime">конечная метрка времени в секундах с 01.01.2021</param>
        /// <returns>Список метрик, которые были сохранены в заданном диапазоне времени</returns>
        /// <response code="201">Если все хорошо</response>
        /// <response code="400">если передали не правильные параетры</response> 
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

            var response = new AllRamMetricsApiResponse()
            {
                Metrics = new List<RamMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<RamMetricDto>(metric));
            }

            return Ok($"По перцентилю {percentile} нагрузка не превышает {metrics.Max(metric => metric.Value)}%");
        }
        /// <summary>
        /// Получает метрики Ram на заданном диапазоне времени
        /// </summary>
        /// <param name="fromTime">начальная метрка времени в секундах с 01.01.2000</param>
        /// <param name="toTime">конечная метрка времени в секундах с 01.01.2021</param>
        /// <returns>Список метрик, которые были сохранены в заданном диапазоне времени</returns>
        /// <response code="201">Если все хорошо</response>
        /// <response code="400">если передали не правильные параетры</response> 
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Входные данные {fromTime} {toTime}");

            var agents = _agent.GetAll();

            if (agents == null)
            {
                return Ok();
            }

            List<RamMetric> ramMetrics = new List<RamMetric>();

            for (int i = 0; i < agents.Count; i++)
            {
                ramMetrics.AddRange(_repository.GetFromToByAgent(agents[i].AgentId, fromTime, toTime));
            }

            if (ramMetrics == null)
            {
                return Ok();
            }

            var response = new AllRamMetricsApiResponse()
            {
                Metrics = new List<RamMetricDto>()
            };

            foreach (var metric in ramMetrics)
            {
                response.Metrics.Add(_mapper.Map<RamMetricDto>(metric));
            }

            return Ok(response);
        }
        /// <summary>
        /// Получает метрики Ram на заданном диапазоне времени с працентли
        /// </summary>
        /// <param name="fromTime">начальная метрка времени в секундах с 01.01.2000</param>
        /// <param name="toTime">конечная метрка времени в секундах с 01.01.2021</param>
        /// <returns>Список метрик, которые были сохранены в заданном диапазоне времени</returns>
        /// <response code="201">Если все хорошо</response>
        /// <response code="400">если передали не правильные параетры</response> 
        [HttpGet("cluster/from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsByPercentileFromAllCluster([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime,
            [FromRoute] Percentile percentile)
        {
            _logger.LogInformation($"Входные данные {fromTime} {toTime} {percentile}");

            var agents = _agent.GetAll();

            if (agents == null)
            {
                return Ok();
            }

            List<RamMetric> ramMetrics = new List<RamMetric>();

            for (int i = 0; i < agents.Count; i++)
            {
                ramMetrics.AddRange(_repository.GetFromToByAgent(agents[i].AgentId, fromTime, toTime));
            }

            if (ramMetrics == null)
            {
                return Ok();
            }

            var response = new AllRamMetricsApiResponse()
            {
                Metrics = new List<RamMetricDto>()
            };

            foreach (var metric in ramMetrics)
            {
                response.Metrics.Add(_mapper.Map<RamMetricDto>(metric));
            }

            return Ok($"По перцентилю {percentile} нагрузка не превышает {ramMetrics.Max(metric => metric.Value)}%");
        }
    }
}
