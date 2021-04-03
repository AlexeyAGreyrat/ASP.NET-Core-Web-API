using System;
using System.Collections.Generic;

namespace MetricAgent.Responses
{
    public class AllNetworkMetricsResponse
    {
        public List<NetworkMetricDto> Metrics { get; set; }
    }

    public class NetworkMetricDto
    {
        public int Id { get; set; }
        public TimeSpan Time { get; set; }
        public int Value { get; set; }
        
    }
}
