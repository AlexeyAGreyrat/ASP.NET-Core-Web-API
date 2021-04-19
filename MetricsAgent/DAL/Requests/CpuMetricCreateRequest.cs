using System;

namespace MetricAgent.DAL.Requests
{
    public class CpuMetricCreateRequest
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }
}
