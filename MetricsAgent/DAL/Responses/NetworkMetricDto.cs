using System;
using System.Collections.Generic;


namespace MetricAgent.DAL.Responses
{
    public class AllNetworkMetricsResponse
    {
        public List<NetworkMetricDto> Metrics { get; set; }
    }
    public class NetworkMetricDto
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }
}
