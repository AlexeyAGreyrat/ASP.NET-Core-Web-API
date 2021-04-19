﻿using System;

namespace MetricManager.DAL.Models
{
    public class RamMetricModel
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public DateTimeOffset Time { get; set; }
        public int IdAgent { get; set; }
    }
}