using MetricManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricManager.DAL.Responses
{
    public class AllHddMetricsApiResponse
    {
        public List<HddMetricDto> Metrics { get; set; }
    }
}
