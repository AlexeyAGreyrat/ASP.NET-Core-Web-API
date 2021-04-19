using AutoMapper;
using MetricAgent.DAL.Interfaces;
using MetricAgent.DAL.Models;
using MetricAgent.DAL.Responses;
using Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MetricAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsAgentController : ControllerBase
    {
        private readonly ILogger<CpuMetricsAgentController> _logger;
        private readonly ICpuMetricsRepository _repository;
        private readonly IMapper _mapper;

        public CpuMetricsAgentController(IMapper mapper, ICpuMetricsRepository repository, ILogger<CpuMetricsAgentController> logger)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _logger.LogDebug( "NLog встроен в CpuMetricsController");
        }
        /// <summary>
        /// Создание новой метрики CPU
        /// </summary>
        /// <response code="200">Удачное выполнение запроса</response>
        /// <response code="400">Ошибка в запросе</response>
        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricModel item)
        {
            _logger.LogDebug("Вызов метода Create");
            _repository.Create(item);
            return Ok();
        }

        /// <summary>
        /// Получение всех метрик CPU
        /// </summary>
        /// <response code="200">Удачное выполнение запроса</response>
        /// <response code="400">Ошибка в запросе</response>
        [HttpGet("all")]
        public IActionResult GetAllMetrics()
        {
            _logger.LogDebug("Вызов метода GetAllMetrics");
            IList<CpuMetricModel> metrics = _repository.GetAll();
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

        /// <summary>
        /// Получение всех метрик CPU в заданном диапазоне времени
        /// </summary>
        /// <param name="fromTime">начальная дата</param>
        /// <param name="toTime">конечная дата</param>
        /// <returns>Список метрик, которые были сохранены в заданном диапазоне времени</returns>
        /// <response code="200">Если все хорошо</response>
        /// <response code="400">если передали не правильные параетры</response>
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromTimeToTime([FromRoute] DateTimeOffset fromTime,[FromRoute] DateTimeOffset toTime)
        {
            _logger.LogDebug("Вызов метода GetMetricsFromTimeToTime");
            IList<CpuMetricModel> metrics = _repository.GetMetricsFromTimeToTime(fromTime, toTime);
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

        /// <summary>
        /// Получение заданного перцентиля для метрик CPU в заданном диапазоне времени
        /// </summary>
        /// <param name="percentile">требуемый перцентиль из Enum Percentile</param>
        /// <param name="fromTime">начальная дата</param>
        /// <param name="toTime">конечная дата</param>
        /// <returns>Список метрик, которые были сохранены в заданном диапазоне времени</returns>
        /// <response code="200">Если все хорошо</response>
        /// <response code="400">если передали не правильные параетры</response>
        [HttpGet("from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsFromTimeToTimePercentile([FromRoute] DateTimeOffset fromTime,[FromRoute] DateTimeOffset toTime,[FromRoute] Percentile percentile)
        {
            _logger.LogDebug("Вызов метода GetMetricsFromTimeToTimePercentile");
            var metrics = _repository.GetMetricsFromTimeToTimeOrderBy(fromTime, toTime, "value");
            if (metrics.Count == 0)
            {
                return NoContent();
            }

            int percentileThisList = (int)percentile;
            percentileThisList = percentileThisList * metrics.Count / 100;

            var response = metrics[percentileThisList].Value;

            return Ok(response);
        }
    }
}
