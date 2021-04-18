using MetricAgent.DAL.Models;
using System.Collections.Generic;

namespace MetricAgent.DAL.Responses
{
    public class AllCpuMetricsResponse
    {
        public List<CpuMetricDto> Metrics { get; set; }
    }
}