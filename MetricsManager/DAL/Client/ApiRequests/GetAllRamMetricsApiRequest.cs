using System;

namespace MetricManager.DAL.Client.ApiRequests
{
    public class GetAllRamMetricsApiRequest
    {
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
        public string Addres { get; set; }
    }
}
