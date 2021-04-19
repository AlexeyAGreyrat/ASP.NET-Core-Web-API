using System;

namespace MetricManager.DAL.Requests
{
    public class HddMetricManagerCreateRequest
    {
        public DateTimeOffset Time { get; set; }
        public double Value { get; set; }
        public int IdAgent { get; set; }
    }
}
