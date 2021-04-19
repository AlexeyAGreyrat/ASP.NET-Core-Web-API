using AutoMapper;
using Core;
using MetricAgent.DAL.Models;
using MetricAgent.DAL.Interfaces;
using MetricAgent.DAL.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;


namespace MetricAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsAgentController : ControllerBase
    {
        private readonly ILogger<RamMetricsAgentController> _logger;
        private readonly IRamMetricsRepository _repository;
        private readonly IMapper _mapper;

        public RamMetricsAgentController(IMapper mapper, IRamMetricsRepository repository, ILogger<RamMetricsAgentController> logger)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Получение всех метрик Ram
        /// </summary>
        /// <response code="200">Удачное выполнение запроса</response>
        /// <response code="400">Ошибка в запросе</response>
        [HttpGet("all")]
        public IActionResult GetMetricsAvailableRam()
        {
            _logger.LogDebug("Вызов метода GetMetricsAvailableRam");
            IList<RamMetricModel> metrics = _repository.GetAll();
            var response = new AllRamMetricsResponse()
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
        /// Получение всех метрик Ram в заданном диапазоне времени
        /// </summary>
        /// <param name="fromTime">начальная дата</param>
        /// <param name="toTime">конечная дата</param>
        /// <returns>Список метрик, которые были сохранены в репозитории и соответствуют заданному диапазону времени</returns>
        /// <response code="200">Удачное выполнение запроса</response>
        /// <response code="400">Ошибка в запросе</response>
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromTimeToTime([FromRoute] DateTimeOffset fromTime,[FromRoute] DateTimeOffset toTime)
        {
            _logger.LogDebug("Вызов метода GetMetricsFromTimeToTime");
            IList<RamMetricModel> metrics = _repository.GetMetricsFromTimeToTime(fromTime, toTime);
            var response = new AllRamMetricsResponse()
            {
                Metrics = new List<RamMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<RamMetricDto>(metric));
            }
            return Ok(response);
        }
    }
}
