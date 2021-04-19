using System;
using System.Collections.Generic;
using Core;
using MetricManager.DAL.Models;

namespace MetricManager.DAL.Interfaces
{
    public interface IHddMetricsRepository : IRepository<HddMetricModel>
    {
        IList<HddMetricModel> GetMetricsFromTimeToTime(DateTimeOffset fromTime, DateTimeOffset toTime);
        IList<HddMetricModel> GetMetricsFromTimeToTimeFromAgent(DateTimeOffset fromTime, DateTimeOffset toTime, int idAgent);
        IList<HddMetricModel> GetMetricsFromTimeToTimeOrderBy(DateTimeOffset fromTime, DateTimeOffset toTime, string sortingField);
        IList<HddMetricModel> GetMetricsFromTimeToTimeFromAgentOrderBy(DateTimeOffset fromTime, DateTimeOffset toTime, string sortingField, int idAgent);
        DateTimeOffset LastTime();
    }
}
