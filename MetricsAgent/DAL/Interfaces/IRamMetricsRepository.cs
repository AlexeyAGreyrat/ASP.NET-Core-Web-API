using System;
using System.Collections.Generic;
using Core;
using MetricAgent.DAL.Models;


namespace MetricAgent.DAL.Interfaces
{
    public interface IRamMetricsRepository : IRepository<RamMetricModel>
    {
        IList<RamMetricModel> GetMetricsFromTimeToTime(DateTimeOffset fromTime, DateTimeOffset toTime);
    }
}
