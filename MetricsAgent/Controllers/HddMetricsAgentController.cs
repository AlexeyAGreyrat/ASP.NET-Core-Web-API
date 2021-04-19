using AutoMapper;
using Core;
using MetricAgent.DAL.Interfaces;
using MetricAgent.DAL.Models;
using MetricAgent.DAL.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;


namespace MetricAgent.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsAgentController : ControllerBase
    {
        private readonly ILogger<HddMetricsAgentController> _logger;
        private readonly IHddMetricsRepository _repository;
        private readonly IMapper _mapper;

        public HddMetricsAgentController(IMapper mapper, IHddMetricsRepository repository, ILogger<HddMetricsAgentController> logger)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Получение всех метрик Hdd
        /// </summary>
        /// <response code="200">Если все хорошо</response>
        /// <response code="400">если передали не правильные параетры</response>
        [HttpGet("all")]
        public IActionResult GetMetricsAvailableHdd()
        {
            _logger.LogDebug("Вызов метода GetMetricsAvailableHdd");
            IList<HddMetricModel> metrics = _repository.GetAll();
            var response = new AllHddMetricsResponse()
            {
                Metrics = new List<HddMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<HddMetricDto>(metric));
            }
            return Ok(response);
        }

        /// <summary>
        /// Получение всех метрик Hdd в заданном диапазоне времени
        /// </summary>       
        /// <param name="fromTime">начальная дата</param>
        /// <param name="toTime">конечная дата</param> 
        /// <returns>Список метрик, которые были сохранены в репозитории и соответствуют заданному диапазону времени</returns>
        /// <response code="200">Если все хорошо</response>
        /// <response code="400">если передали не правильные параетры</response>
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromTimeToTime([FromRoute] DateTimeOffset fromTime,[FromRoute] DateTimeOffset toTime)
        {
            _logger.LogDebug("Вызов метода GetMetricsFromTimeToTime");
            IList<HddMetricModel> metrics = _repository.GetMetricsFromTimeToTime(fromTime, toTime);
            var response = new AllHddMetricsResponse()
            {
                Metrics = new List<HddMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<HddMetricDto>(metric));
            }

            return Ok(response);
        }
    }
}
