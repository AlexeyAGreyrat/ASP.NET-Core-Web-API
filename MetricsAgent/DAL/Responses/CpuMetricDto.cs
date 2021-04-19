using System;
using System.Collections.Generic;


namespace MetricAgent.DAL.Responses
{
    public class AllCpuMetricsResponse
    {
        public List<CpuMetricDto> Metrics { get; set; }
    }
    public class CpuMetricDto
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}
