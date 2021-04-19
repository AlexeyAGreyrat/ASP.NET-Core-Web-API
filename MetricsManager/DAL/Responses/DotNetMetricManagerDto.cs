using System;
using System.Collections.Generic;


namespace MetricManager.DAL.Responses
{
    public class AllDotNetMetricsResponse
    {
        public List<DotNetMetricManagerDto> Metrics { get; set; }
    }
    public class DotNetMetricManagerDto
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
        public int IdAgent { get; set; }
    }
}
