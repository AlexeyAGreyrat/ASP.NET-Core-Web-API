using Core;
using MetricAgent.DAL.Models;
using System;
using System.Collections.Generic;

namespace MetricAgent.DAL.Interfaces
{
    public interface ICpuMetricsRepository : IRepository<CpuMetricModel>
    {
        IList<CpuMetricModel> GetMetricsFromTimeToTime(DateTimeOffset fromTime, DateTimeOffset toTime);
        IList<CpuMetricModel> GetMetricsFromTimeToTimeOrderBy(DateTimeOffset fromTime, DateTimeOffset toTime, string sortingField);
    }
}
