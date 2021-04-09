using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.IO;
using MetricAgent.Metric;
using AutoMapper;
using MetricAgent.Responses;
using MetricAgent;
using MetricAgent.Interface;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;
        private IRepository<HddMetric> _repository;
        private IMapper _mapper;

        public HddMetricsController(ILogger<HddMetricsController> logger, IRepository<HddMetric> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в HddMetricsController");
        }
        [HttpGet("left")]
        public IActionResult GetHdd()
        {
            _logger.LogInformation("HddNetMetricsController вызов метода GetHdd");
            double free = 0;
            double Driver = 0;
            string nameSpace = "";
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo MyDriveInfo in allDrives)
            {
                if (MyDriveInfo.IsReady == true)
                {
                    free = MyDriveInfo.AvailableFreeSpace;
                    Driver = (free / 1024) / 1024;
                    nameSpace += MyDriveInfo.Name + ": " + Driver.ToString("#.##") + Environment.NewLine;
                }
            }
            return Ok(nameSpace);
        }       
    }
}
