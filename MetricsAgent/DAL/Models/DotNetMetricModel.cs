﻿using System;

namespace MetricAgent.DAL.Models
{
    public class DotNetMetricModel
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}
