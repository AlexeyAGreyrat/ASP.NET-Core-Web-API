using System;
using System.Collections.Generic;


namespace MetricManager.DAL.Responses
{
    public class AllCpuMetricsResponse
    {
        public List<CpuMetricManagerDto> Metrics { get; set; }
    }
    public class CpuMetricManagerDto
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
        public int IdAgent { get; set; }
    }
}
