using System;

namespace MetricManager.DAL.Client.ApiRequests
{ 
    public class GetAllDotNetMetricsApiRequest
    {
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
        public string Addres { get; set; }
    }
}
