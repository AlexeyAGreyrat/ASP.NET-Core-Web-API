using MetricManager.DAL.DTO;
using AutoMapper;
using Core.Interfaces;
using MetricManager.DAL.Models;
using MetricManager.DAL.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricManager.Controllers
    {
    [Route("api/agents")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly ILogger<AgentsController> _logger;
        private IAgentsRepository<AgentInfo> _agent;
        private IMapper _mapper;

        public AgentsController(ILogger<AgentsController> logger,IAgentsRepository<AgentInfo> agent, IMapper mapper)
        {
            _mapper = mapper;
            _agent = agent;
            _logger = logger;
            _logger.LogInformation("NLog зарегистрирован в AgentsController");
        }
        /// <summary>
        /// Получает агентов
        /// </summary>

        /// <returns>Список агентов, которые были сохранены </returns>
        /// <response code="201">Если все хорошо</response>
        /// <response code="400">если передали не правильные параетры</response> 
        [HttpGet("read")]
        public IActionResult ReadRegisteredAgents()
        {
            _logger.LogInformation("NLog вызван в ReadRegisteredAgents");
            IList<AgentInfo> agents = _agent.GetAll();

            if (agents == null)
            {
                return Ok();
            }

            var response = new AllAgentsResponce()
            {
                Agents = new List<AgentDto>()
            };

            foreach (var agent in agents)
            {
                response.Agents.Add(_mapper.Map<AgentDto>(agent));
            }

            return Ok(response);
        }
        /// <summary>
        /// регестрируем агентов
        /// </summary>

        /// <param name="agentInfo">регестрируем агента</param>
        /// <response code="201">Если все хорошо</response>
        /// <response code="400">если передали не правильные параетры</response> 
        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            _logger.LogInformation($"Входные данные: {agentInfo}");
            return Ok();
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation($"Входные данные: {agentId}");
            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation($"Входные данные: {agentId}");
            return Ok();
        }
    }
}

