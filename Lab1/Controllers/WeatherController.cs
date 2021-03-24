using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase 
    {
        private readonly ValuesHolder holder;

        public WeatherController(ValuesHolder holder)
        {
            this.holder = holder;
        }

        [HttpPost("create")]
        public IActionResult Create([FromQuery] WeatherForecast weatherToDay)
        {            
            holder.Values.Add(weatherToDay);
            return Ok();
        }

        [HttpGet("read")]
        public IActionResult Read([FromQuery] DateTime? dateStart, [FromQuery] DateTime? dateEnd)
        {
           var values = holder.Values.Where(Whether => Whether.Date >= dateStart && Whether.Date <= dateEnd).ToList();           
            return Ok(values);
        }

        [HttpPut("update")]
        public IActionResult Update([FromQuery] WeatherForecast weatherToDay)
        {
            for (int i = 0; i < holder.Values.Count; i++)
            {
                if (holder.Values[i].Date == weatherToDay.Date)
                {
                    holder.Values[i].TemperatureC = weatherToDay.TemperatureC;
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
            holder.Values.RemoveAll(Whether => Whether.Date >= dateStart && Whether.Date <= dateEnd);
            return Ok();
        }
    }
}
        
