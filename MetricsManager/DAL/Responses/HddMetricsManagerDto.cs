using System;
using System.Collections.Generic;


namespace MetricManager.DAL.Responses
{
    public class AllHddMetricsResponse
    {
        public List<HddMetricManagerDto> Metrics { get; set; }
    }
    public class HddMetricManagerDto
    {
        public DateTimeOffset Time { get; set; }
        public double Value { get; set; }
        public int Id { get; set; }
        public int IdAgent { get; set; }
    }
}
