using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Core.Enum;
using MetricManager.DAL.Client;
using Core.Interfaces;
using AutoMapper;
using MetricManager.DAL.DTO;
using MetricManager.DAL.Metrics;
using MetricManager.DAL.Responses;
using MetricManager.DAL.Models;

namespace MetricManager.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : Controller
    {
        private readonly ILogger<NetworkMetricsController> _logger;
        private IMetricsAgentClient _metricsAgentClient;
        private IRepositoryGet<NetworkMetric> _repository;
        private IMapper _mapper;
        private IAgentsRepository<AgentInfo> _agent;

        public NetworkMetricsController(ILogger<NetworkMetricsController> logger, IMetricsAgentClient metricsAgentClient,
            IRepositoryGet<NetworkMetric> repository, IMapper mapper, IAgentsRepository<AgentInfo> agent)
        {
            _agent = agent;
            _mapper = mapper;
            _repository = repository;
            _metricsAgentClient = metricsAgentClient;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в NetworkMetricsController");
        }
        /// <summary>
        /// Получает метрики Network на заданном диапазоне времени
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

            var response = new AllNetworkMetricsApiResponce()
            {
                Metrics = new List<NetworkMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<NetworkMetricDto>(metric));
            }

            return Ok(response);
        }
        /// <summary>
        /// Получает метрики Network на заданном диапазоне времени с працентли
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

            var response = new AllNetworkMetricsApiResponce()
            {
                Metrics = new List<NetworkMetricDto>()
            };           

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<NetworkMetricDto>(metric));
            }

            return Ok($"По перцентилю {percentile} нагрузка не превышает {metrics.Max(metric => metric.Value)}%");
        }
        /// <summary>
        /// Получает метрики Network на заданном диапазоне времени
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

            List<NetworkMetric> networkMetrics = new List<NetworkMetric>();

            for (int i = 0; i < agents.Count; i++)
            {
                networkMetrics.AddRange(_repository.GetFromToByAgent(agents[i].AgentId, fromTime, toTime));
            }

            if (networkMetrics == null)
            {
                return Ok();
            }

            var response = new AllNetworkMetricsApiResponce()
            {
                Metrics = new List<NetworkMetricDto>()
            };

            foreach (var metric in networkMetrics)
            {
                response.Metrics.Add(_mapper.Map<NetworkMetricDto>(metric));
            }

            return Ok(response);
        }
        /// <summary>
        /// Получает метрики Network на заданном диапазоне времени с працентли
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

            List<NetworkMetric> networkMetrics = new List<NetworkMetric>();

            for (int i = 0; i < agents.Count; i++)
            {
                networkMetrics.AddRange(_repository.GetFromToByAgent(agents[i].AgentId, fromTime, toTime));
            }

            if (networkMetrics == null)
            {
                return Ok();
            }

            var response = new AllNetworkMetricsApiResponce()
            {
                Metrics = new List<NetworkMetricDto>()
            };

            foreach (var metric in networkMetrics)
            {
                response.Metrics.Add(_mapper.Map<NetworkMetricDto>(metric));
            }

            return Ok($"По перцентилю {percentile} нагрузка не превышает {networkMetrics.Max(metric => metric.Value)}%");
        }
    }
}
