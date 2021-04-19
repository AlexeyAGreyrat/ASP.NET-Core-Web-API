using System;

namespace MetricManager.DAL.Client.ApiRequests
{
    public class GetAllCpuMetricsApiRequest
    {
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
        public string Addres { get; set; }
    }
}
