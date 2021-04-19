using MetricManager.DAL.Client;
using MetricManager.DAL.Client.ApiRequests;
using MetricManager.DAL.Interfaces;
using MetricManager.DAL.Models;
using MetricManager.DAL.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MetricsManager.Controllers
{

    [Route("api/agents")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly ILogger<AgentsController> _logger;
        private readonly IAgentsRepository _repository;

        public AgentsController(IAgentsRepository repository, ILogger<AgentsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }       

        /// <summary>
        /// Регистрация нового агента для сбора сбора метрик
        /// </summary>
        /// <response code="200">Удачное выполнение запроса</response>
        /// <response code="400">Ошибка в запросе</response>
        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentModel agentInfo)
        {
            _logger.LogDebug("Вызов метода RegisterAgent");
            _repository.Create(agentInfo);

            _logger.LogInformation($"Регистрация агента: ");
                                   

            return Ok();
        }

        /// <summary>
        /// Включение агента сбора метрик
        /// </summary>
        /// <response code="200">Удачное выполнение запроса</response>
        /// <response code="400">Ошибка в запросе</response>
        [HttpPut("enable/{Idagent}")]
        public IActionResult EnableAgentById([FromRoute] int Idagent)
        {
            _logger.LogDebug("Вызов метода EnableAgentById");
            AgentModel agent = _repository.GetById(Idagent);
            agent.Status = true;
            _repository.Update(agent);

            _logger.LogInformation($"Включение агента Id = {Idagent}");

            return Ok();
        }

        /// <summary>
        /// Выключение агента сбора метрик
        /// </summary>
        /// <response code="200">Удачное выполнение запроса</response>
        /// <response code="400">Ошибка в запросе</response>
        [HttpPut("disable/{Idagent}")]
        public IActionResult DisableAgentById([FromRoute] int Idagent)
        {
            _logger.LogDebug("Вызов метода DisableAgentById");
            AgentModel agent = _repository.GetById(Idagent);
            agent.Status = false;
            _repository.Update(agent);

            _logger.LogInformation($"Отключение агента Id = {Idagent}");

            return Ok();
        }

        /// <summary>
        /// Получение всех зарегистрированных агентов
        /// </summary>
        /// <response code="200">Удачное выполнение запроса</response>
        /// <response code="400">Ошибка в запросе</response>
        [HttpGet("all")]
        public IActionResult GetAllAgents()
        {
            _logger.LogDebug("Вызов метода GetAllAgents");
            var metrics = _repository.GetAll();
            var response = new AllAgentsResponse()
            {
                Metrics = new List<AgentManagerDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new AgentManagerDto
                {
                    Id = metric.Id,
                    Status = metric.Status,
                    Ipaddress = metric.Ipaddress,
                    Name = metric.Name
                });
            }

            _logger.LogInformation("Запрос всех агентов");


            return Ok(response);
        }
    }
}
