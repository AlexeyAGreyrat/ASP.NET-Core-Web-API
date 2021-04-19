using System;

namespace MetricAgent.DAL.Requests
{
    public class DotNetMetricCreateRequest
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
    }
}
