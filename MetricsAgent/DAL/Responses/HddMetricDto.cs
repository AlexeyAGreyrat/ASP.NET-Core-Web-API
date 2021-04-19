using System;
using System.Collections.Generic;


namespace MetricAgent.DAL.Responses
{
    public class AllHddMetricsResponse
    {
        public List<HddMetricDto> Metrics { get; set; }
    }
    public class HddMetricDto
    {
        public DateTimeOffset Time { get; set; }
        public double FreeSize { get; set; }
        public int Id { get; set; }
    }
}
