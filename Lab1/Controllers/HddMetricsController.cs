using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace Lab1.Controllers
{
    [Route("api/metrics/Hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        [HttpGet("left")]
        public IActionResult GetHdd()
        {
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
