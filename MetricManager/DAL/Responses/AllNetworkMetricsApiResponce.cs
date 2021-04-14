using MetricManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricManager.DAL.Responses
{
    public class AllNetworkMetricsApiResponce
    {
        public List<NetworkMetricDto> Metrics { get; set; }
    }
}
