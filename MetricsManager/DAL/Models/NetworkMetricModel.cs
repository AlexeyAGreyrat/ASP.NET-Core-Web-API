﻿using System;

namespace MetricManager.DAL.Models
{
    public class NetworkMetricModel
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }
        public int IdAgent { get; set; }
    }
}
