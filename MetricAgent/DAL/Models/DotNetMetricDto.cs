using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricAgent.DAL.Models
{
    public class DotNetMetricDto
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }
}
