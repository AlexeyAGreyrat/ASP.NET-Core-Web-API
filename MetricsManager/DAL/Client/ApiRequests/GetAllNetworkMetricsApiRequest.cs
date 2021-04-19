using System;

namespace MetricManager.DAL.Client.ApiRequests
{ 
    public class GetAllNetworkMetricsApiRequest
    {
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
        public string Addres { get; set; }
    }
}
