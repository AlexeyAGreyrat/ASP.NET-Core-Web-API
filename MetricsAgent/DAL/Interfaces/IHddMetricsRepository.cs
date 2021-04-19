using System;
using System.Collections.Generic;
using Core;
using MetricAgent.DAL.Models;


namespace MetricAgent.DAL.Interfaces
{
    public interface IHddMetricsRepository : IRepository<HddMetricModel>
    {
        IList<HddMetricModel> GetMetricsFromTimeToTime(DateTimeOffset fromTime, DateTimeOffset toTime);
    }
}
