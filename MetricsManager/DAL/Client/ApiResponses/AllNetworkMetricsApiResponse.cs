using System;
using System.Collections.Generic;


namespace MetricManager.DAL.Client.ApiResponses
{
    public class AllNetworkMetricsApiResponse
    {
        public List<NetworkMetricApiResponse> Metrics { get; set; }
    }
    public class NetworkMetricApiResponse
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
        public int IdAgent { get; set; }
    }
}
