using System;
using System.Collections.Generic;


namespace MetricManager.DAL.Client.ApiResponses
{
    public class AllDotNetMetricsApiResponse
    {
        public List<DotNetMetricApiResponse> Metrics { get; set; }
    }
    public class DotNetMetricApiResponse
    {
        public DateTimeOffset Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
        public int IdAgent { get; set; }
    }
}
