using System;
using System.Collections.Generic;
using Core;
using MetricManager.DAL.Models;

namespace MetricManager.DAL.Interfaces
{
    public interface IRamMetricsRepository : IRepository<RamMetricModel>
    {
        IList<RamMetricModel> GetMetricsFromTimeToTime(DateTimeOffset fromTime, DateTimeOffset toTime);
        IList<RamMetricModel> GetMetricsFromTimeToTimeFromAgent(DateTimeOffset fromTime, DateTimeOffset toTime, int idAgent);
        IList<RamMetricModel> GetMetricsFromTimeToTimeOrderBy(DateTimeOffset fromTime, DateTimeOffset toTime, string sortingField);
        IList<RamMetricModel> GetMetricsFromTimeToTimeFromAgentOrderBy(DateTimeOffset fromTime, DateTimeOffset toTime, string sortingField, int idAgent);
        DateTimeOffset LastTime();
    }
}
