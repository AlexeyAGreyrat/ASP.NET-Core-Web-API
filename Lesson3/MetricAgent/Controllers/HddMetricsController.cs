using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using MetricManager.Enums;
using System.Data.SQLite;
using MetricAgent.DAL;
using MetricAgent.Model;
using MetricAgent.Requests;
using MetricAgent.Responses;
using Microsoft.Extensions.Logging;
using MetricAgent.DTO;
using System.Collections.Generic;
using System.IO;

namespace MetricAgent.Controllers
{
    [Route("api/metrics/Hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;
        private readonly IHddMetricsRepository _repository;

        public HddMetricsController(ILogger<HddMetricsController> logger, IHddMetricsRepository repository)
        {
            _repository = repository;
            _logger = logger;
            _logger.LogInformation("NLog встроен в HddMetricsController");
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
