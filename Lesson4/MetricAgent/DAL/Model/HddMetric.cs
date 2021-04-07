using System;

namespace MetricAgent.Model
{
    public class HddMetric
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public TimeSpan Time { get; set; }
    }
}
