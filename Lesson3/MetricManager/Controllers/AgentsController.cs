using MetricManager.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

    namespace MetricManager.Controllers
    {
    [Route("api/agents")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly ILogger<AgentsController> _logger;

        public AgentsController(ILogger<AgentsController> logger)
        {
            _logger = logger;
            _logger.LogInformation("NLog зарегистрирован в AgentsController");
        }


        [HttpGet("read")]
        public IActionResult ReadRegisteredAgents()
        {
            _logger.LogInformation("NLog вызван в ReadRegisteredAgents");
            return Ok();
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            _logger.LogInformation("NLog вызван в RegisterAgent");
            return Ok();
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation("NLog вызван в EnableAgentById");
            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation("NLog вызван в  DisableAgentById");
            return Ok();
        }
    }
}

