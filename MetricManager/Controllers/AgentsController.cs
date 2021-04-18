using AutoMapper;
using Dapper;
using Core.Enum;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using MetricManager.DAL.DTO;
using MetricManager.DAL.Responses;
using MetricManager.DAL.Models;

namespace MetricManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly ILogger<AgentsController> _logger;
        private IAgentsRepository<AgentInfo> _agent;
        private IMapper _mapper;
        private const string ConnectionString = @"Data Source=metrics.db; Version=3;Pooling=True;Max Pool Size=100;";

        public AgentsController(ILogger<AgentsController> logger, IAgentsRepository<AgentInfo> agent, IMapper mapper)
        {
            _mapper = mapper;
            _agent = agent;
            _logger = logger;
            _logger.LogDebug("NLog встроен в AgentsController");
        }
        /// <summary>
        /// Регестрируем агента
        /// </summary>
        /// <param name="agentInfo">регестрируем агента</param>
        /// <response code="200">Если все хорошо</response>
        /// <response code="400">если передали не правильные параетры</response> 
        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {  
            try
            {
                _agent.Create(agentInfo);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
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

        [HttpGet("read")]
        public IActionResult ReadRegisteredAgents()
        {
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
    }
}
