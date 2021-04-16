using MetricAgent.Responses;
using System;
using System.Collections.Generic;

namespace MetricAgent.Controllers
{
    public class AllCpuMetricsResponse
    {
        public List<CpuMetricDto> Metrics { get; set; }
    }
}