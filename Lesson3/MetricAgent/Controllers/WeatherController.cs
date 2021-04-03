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
using System.Linq;

namespace MetricAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase 
    {
        private readonly ValuesHolder _holder;

        public WeatherController(ValuesHolder holder)
        {
            this._holder = holder;
        }

        [HttpPost("create")]
        public IActionResult Create([FromQuery] WeatherForecast weatherToDay)
        {            
            _holder.Values.Add(weatherToDay);
            return Ok();
        }

        [HttpGet("read")]
        public IActionResult Read([FromQuery] DateTime? dateStart, [FromQuery] DateTime? dateEnd)
        {
           var values = _holder.Values.Where(Whether => Whether.Date >= dateStart && Whether.Date <= dateEnd).ToList();           
            return Ok(values);
        }

        [HttpPut("update")]
        public IActionResult Update([FromQuery] WeatherForecast weatherToDay)
        {
            for (int i = 0; i < _holder.Values.Count; i++)
            {
                if (_holder.Values[i].Date == weatherToDay.Date)
                {
                    _holder.Values[i].TemperatureC = weatherToDay.TemperatureC;
                }
            }
            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] DateTime dateStart, [FromQuery] DateTime dateEnd)
        {
            if (dateEnd <= dateStart)
            {
                return BadRequest();
            }
            _holder.Values.RemoveAll(Whether => Whether.Date >= dateStart && Whether.Date <= dateEnd);
            return Ok();
        }
    }
}
        
