using System;

namespace MetricAgent.DAL.Models
{
    public class HddMetricModel
    {
        public int Id { get; set; }
        public double FreeSize { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}
