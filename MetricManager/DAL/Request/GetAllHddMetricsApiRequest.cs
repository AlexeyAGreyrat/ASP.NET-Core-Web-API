using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricManager.DAL.Request
{
    public class GetAllHddMetricsApiRequest
    {
        public string ClientBaseAddress { get; set; }
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
    }
}
