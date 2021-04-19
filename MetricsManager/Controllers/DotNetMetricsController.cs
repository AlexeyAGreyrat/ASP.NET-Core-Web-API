using AutoMapper;
using Core;
using MetricManager.DAL.Interfaces;
using MetricManager.DAL.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using MetricManager.DAL.Models;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsController> _logger;
        private readonly IDotNetMetricsRepository _repository;
        private readonly IMapper _mapper;

        public DotNetMetricsController(IMapper mapper, IDotNetMetricsRepository repository, ILogger<DotNetMetricsController> logger)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        /// <summary>
        /// проверка метрик
        /// </summary>
        /// <returns>Список метрик, которые были сохранены </returns>
        /// <response code="200">Удачное выполнение запроса</response>
        /// <response code="400">Ошибка в запросе</response>
        [HttpGet("all")]
        public IActionResult GetAllMetrics()
        {
            _logger.LogDebug("Вызов метода GetAllMetrics");
            IList<DotNetMetricModel> metrics = _repository.GetAll();
            var response = new AllDotNetMetricsResponse()
            {
                Metrics = new List<DotNetMetricManagerDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<DotNetMetricManagerDto>(metric));
            }
            return Ok(response);
        }
        /// <summary>
        /// Получение всех метрик DotNet в заданном диапазоне времени для указанного агента
        /// </summary>
        /// <param name="fromTime">начальная метка времени в формате DateTimeOffset</param>
        /// <param name="toTime">конечная метка времени в формате DateTimeOffset</param>
        /// <param name="idAgent">Id агента</param>
        /// <returns>Список метрик, которые были сохранены в репозитории и соответствуют заданному диапазону времени</returns>
        /// <response code="200">Удачное выполнение запроса</response>
        /// <response code="400">Ошибка в запросе</response>
        [HttpGet("agent/{idAgent}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int idAgent,[FromRoute] DateTimeOffset fromTime,[FromRoute] DateTimeOffset toTime)
        {
            _logger.LogDebug("Вызов метода GetMetricsFromAgent");
            var metrics = _repository.GetMetricsFromTimeToTimeFromAgent(fromTime, toTime, idAgent);
            var response = new AllDotNetMetricsResponse()
            {
                Metrics = new List<DotNetMetricManagerDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<DotNetMetricManagerDto>(metric));
            }


            return Ok(response);
        }

        /// <summary>
        /// Получение перцинтиля DotNet в заданном диапазоне времени для указанного агента
        /// </summary>
        /// <param name="fromTime">начальная метка времени в формате DateTimeOffset</param>
        /// <param name="toTime">конечная метка времени в формате DateTimeOffset</param>
        /// <param name="idAgent">Id агента</param>
        /// <returns>Указанный  перцинтиль</returns>
        /// <response code="200">Удачное выполнение запроса</response>
        /// <response code="400">Ошибка в запросе</response>
        [HttpGet("agent/{idAgent}/from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsByPercentileFromAgent([FromRoute] int idAgent,[FromRoute] DateTimeOffset fromTime,[FromRoute] DateTimeOffset toTime,[FromRoute] Percentile percentile)
        {
            _logger.LogDebug("Вызов метода GetMetricsByPercentileFromAgent");
            var metrics = _repository.GetMetricsFromTimeToTimeFromAgentOrderBy(fromTime, toTime, "value", idAgent);
            if (metrics.Count == 0) return NoContent();

            int percentileThisList = (int)percentile;
            percentileThisList = percentileThisList * metrics.Count / 100;

            var response = metrics[percentileThisList].Value;           

            return Ok(response);
        }

        /// <summary>
        /// Получение всех метрик DotNet в заданном диапазоне времени для кластера
        /// </summary>
        /// <param name="fromTime">начальная метка времени в формате DateTimeOffset</param>
        /// <param name="toTime">конечная метка времени в формате DateTimeOffset</param>
        /// <returns>Список метрик, которые были сохранены в репозитории и соответствуют заданному диапазону времени</returns>
        /// <response code="200">Удачное выполнение запроса</response>
        /// <response code="400">Ошибка в запросе</response>
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromCluster([FromRoute] DateTimeOffset fromTime,[FromRoute] DateTimeOffset toTime)
        {
            _logger.LogDebug("Вызов метода GetMetricsFromCluster");
            var metrics = _repository.GetMetricsFromTimeToTime(fromTime, toTime);
            var response = new AllDotNetMetricsResponse()
            {
                Metrics = new List<DotNetMetricManagerDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<DotNetMetricManagerDto>(metric));
            }          

            return Ok(response);
        }

        /// <summary>
        /// Получение перцинтиля DotNet в заданном диапазоне времени для всего кластера
        /// </summary>
        /// <param name="fromTime">начальная метка времени в формате DateTimeOffset</param>
        /// <param name="toTime">конечная метка времени в формате DateTimeOffset</param>
        /// <returns>Указанный  перцинтиль</returns>
        /// <response code="200">Удачное выполнение запроса</response>
        /// <response code="400">Ошибка в запросе</response>
        [HttpGet("cluster/from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsByPercentileFromCluster([FromRoute] DateTimeOffset fromTime,[FromRoute] DateTimeOffset toTime,[FromRoute] Percentile percentile)
        {
            _logger.LogDebug("Вызов метода GetMetricsByPercentileFromCluster");
            var metrics = _repository.GetMetricsFromTimeToTimeOrderBy(fromTime, toTime, "value");
            if (metrics.Count == 0) return NoContent();

            int percentileThisList = (int)percentile;
            percentileThisList = percentileThisList * metrics.Count / 100;

            var response = metrics[percentileThisList].Value;

            return Ok(response);
        }
    }
}
