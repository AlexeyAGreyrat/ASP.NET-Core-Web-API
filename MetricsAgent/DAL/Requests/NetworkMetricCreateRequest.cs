using System;

namespace MetricAgent.DAL.Requests
{
    public class NetworkMetricCreateRequest
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }
}
